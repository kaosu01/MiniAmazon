using MiniAmazon.Library.Models;
using System.Collections.ObjectModel;

namespace MiniAmazon.Library.Services
{
    public class InventoryService
    {
        private List<Product> products;
        private InventoryService() { products = new List<Product>(); }
        private static InventoryService? instance;
        private static object instanceLock = new object();
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
        
        public ReadOnlyCollection<Product>? Products
        {
            get { return products.AsReadOnly(); }
        }

        private int LastId
        {
            get
            {
                if (!products.Any())
                    return 1;
                return products.Select(p => p.Id).Max() + 1;
            }
        }
        public Product? AddorUpdate(Product product)
        {
            if (products == null)
                return null;

            var isAdd = false;

            // If This is the First Time this Item is Being Added Into the Inventory
            if (product.Id == 0)
            {
                product.Id = LastId;
                isAdd = true;
            }

            // We Need to Add the Product
            if (isAdd)
                products.Add(product);

            return product;
        }
        public void Delete(int id)
        {
            if (products == null)
                return;

            var productToDelete = products?.FirstOrDefault(c => c.Id == id);

            if (productToDelete != null)
                products?.Remove(productToDelete);
        }
    }
}