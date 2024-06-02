using MiniAmazon.Library.Models;
using System.Collections.ObjectModel;

namespace MiniAmazon.Library.Services 
{
    public class ShoppingCartProxy
    {
        private ShoppingCartProxy() { shopping_Cart = new ShoppingCart(); }
        private static ShoppingCartProxy? instance;
        private static object instanceLock = new object();
        public static ShoppingCartProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartProxy();
                    }
                }
                return instance;
            }
        }
        private ShoppingCart? shopping_Cart;
        public ReadOnlyCollection<Product>? CartProducts
        {
            get { return shopping_Cart?.Items?.AsReadOnly(); }
        }
        public Product? AddorUpdate(Product product)
        {
            if (shopping_Cart == null)
                return null;
            
            var isAdd = false;

            // If This is the First Time this Item is Being Added Into the Shopping Cart
            if (shopping_Cart.Items?.FirstOrDefault(c => c.Id == product.Id) == null)
                isAdd = true;

            // We Need to Add the Product
            if (isAdd)
                shopping_Cart.Items?.Add(product);
            return product;
        }
        public void Delete(int id)
        {
            if (shopping_Cart == null)
                return;

            var productToDelete = shopping_Cart.Items?.FirstOrDefault(c => c.Id == id);

            if(productToDelete != null)
            {
                shopping_Cart.Items?.Remove(productToDelete);
            }
        }
        public void ClearCart()
        {
            shopping_Cart?.Items?.Clear();
        }
    }
}
