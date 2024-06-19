using MiniAmazon.Library.Models;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace MiniAmazon.Library.Services
{
    public class InventoryService
    {
        private static InventoryService? instance;

        // To Support Multi-Threading
        private static object instanceLock = new object();

        // Create A List of Products to Store In Inventory
        private List<Product> products;

        // Make Read Only
        public ReadOnlyCollection<Product>? Products
        {
            get { return products.AsReadOnly(); }
        }
        
        public static InventoryService Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryService();
                    }
                }
                return instance;
            }
        }

        private InventoryService()
        {
            // Added Products For Testing Purposes
            products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Chair",
                    Description = "You sit on it",
                    Price = 19.99m,
                    Quantity = 3
                },
                new Product
                {
                    Id = 2,
                    Name = "Eraser",
                    Description = "You erase with it",
                    Price = 4.99m,
                    Quantity = 7
                }
            };
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
            var prod_In_Cart = ShoppingCartService.Current.Cart.Items;

            for (int i = 0; i < prod_In_Cart?.Count; i++)
            {
                if (prod_In_Cart[i].Id == p.Id)
                {
                    prod_In_Cart[i].Name = p.Name;
                    prod_In_Cart[i].Description = p.Description;
                    prod_In_Cart[i].Price = p.Price;
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