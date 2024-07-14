using MiniAmazon.Library.Models;
using MiniAmazon.Library.Utilities;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace MiniAmazon.Library.Services
{
    public class InventoryServiceProxy
    {
        private static InventoryServiceProxy? instance;

        // To Support Multi-Threading
        private static object instanceLock = new object();

        // Create A List of Products to Store In Inventory
        private List<Product> products;

        // Make Read Only
        public ReadOnlyCollection<Product>? Products
        {
            get { return products.AsReadOnly(); }
        }
        
        public static InventoryServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryServiceProxy();
                    }
                }
                return instance;
            }
        }

        private InventoryServiceProxy()
        {
            var response = new WebRequestHandler().Get("/Inventory").Result;
            products = JsonConvert.DeserializeObject<List<Product>>(response);
        }

        // Get Next Id in the List
        private int LastId
        {
            get
            {
                if (!products.Any())
                    return 1;
                return products.Select(p => p.Id).Max() + 1;
            }
        }
        public Product? AddorUpdate(Product p)
        {
            if (products == null)
                return null;

            var isAdd = false;

            // If This is the First Time this Item is Being Added Into the Inventory
            if (p.Id == 0)
            {
                p.Id = LastId;
                isAdd = true;
            }

            // We Need to Add the Product
            if (isAdd)
            {
                p.Name = p.Name?.ToLower();
                p.Name = char.ToUpper(p.Name[0]) + p.Name[1..];
                products.Add(p);
            }

            // Update the Product in Cart if Changes Were Made
            var carts = ShoppingCartServiceProxy.Current.Carts;
            for (int i = 0; i < carts.Count; i++)
            {
                // Get Product In Cart if In Cart List
                var prod_In_Cart = carts[i].Items?.FirstOrDefault(cartItem => cartItem.Id == p.Id);

                if (prod_In_Cart != null)
                {
                
                    prod_In_Cart.Name = p.Name;
                    prod_In_Cart.Description = p.Description;
                    prod_In_Cart.Price = p.Price;
                    prod_In_Cart.IsMarkdown = p.IsMarkdown;
                    prod_In_Cart.MarkdownPrice = p.MarkdownPrice;
                    prod_In_Cart.IsBOGO = p.IsBOGO;
                } 
            } 

            return p;
        }
        public void Remove(int id)
        {
            if (products == null)
                return;

            var productToDelete = products?.FirstOrDefault(c => c.Id == id);

            if (productToDelete != null)
                products?.Remove(productToDelete);
        }
    }
}