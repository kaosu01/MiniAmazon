using MiniAmazon.Library.Models;
using System.Collections.ObjectModel;

namespace MiniAmazon.Library.Services
{
    public class ProductServiceProxy
    {
        private ProductServiceProxy() { products = new List<Product>(); }
        private static ProductServiceProxy? instance;
        private static object instanceLock = new object();
        public static ProductServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                    if (instance == null)
                    {
                        instance = new ProductServiceProxy();
                    }
                return instance;
            }
        }
        private List<Product>? products;
        public ReadOnlyCollection<Product>? Products
        {
            get { return products?.AsReadOnly(); }
        }

        public int LastId
        {
            get
            {
                if (products?.Any() ?? false)
                {
                    return products?.Select(c => c.Id)?.Max() ?? 0;
                }
                return 0;
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
                product.Id = LastId + 1;
                isAdd = true;
            }

            // We Need to Add the Product
            if (isAdd)
                products?.Add(product);

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