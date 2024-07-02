using MiniAmazon.Library.Models;
using System.Collections.ObjectModel;

namespace MiniAmazon.Library.Services 
{
    public class ShoppingCartServiceProxy
    {
        private ShoppingCartServiceProxy()
        {
            carts = new List<ShoppingCart>();
        }

        private static ShoppingCartServiceProxy? instance;
        private static object instanceLock = new object();


        private List<ShoppingCart> carts;
        public ShoppingCart Cart
        {
            get
            {
                if (!carts.Any())
                {
                    var newCart = new ShoppingCart();
                    carts.Add(newCart);
                    return newCart;
                }
                return carts?.FirstOrDefault() ?? new ShoppingCart();
            }
        }

        public static ShoppingCartServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartServiceProxy();
                    }
                }
                return instance;
            }
        }

        public ReadOnlyCollection<ShoppingCart> Carts
        {
            get
            {
                return carts.AsReadOnly();
            }
        }

        /*
         * private int NextId
        {
            get
            {
                if (!carts.Any())
                    return 1;
                return carts.Select(c => c.Id).Max() + 1;
            }
        }
        */
        /*
        public ShoppingCart AddOrUpdate(ShoppingCart sc)
        {
            bool isAdd = false;

            // Check If A New Shopping Cart is Being Created
            if (sc.Id == 0)
            {
                isAdd = true;
                sc.Id = NextId;
            }

            if (isAdd)
                carts.Add(sc);

            return sc;
        }
        */

        /*
        public void DeleteCart(int id)
        {
            if (Cart == null)
                return;

            // Find the Cart the User Wants to Delete
            var cartToDelete = carts.FirstOrDefault(c => c.Id == id);

            if(cartToDelete != null)
                carts.Remove(cartToDelete);
        }
        */

        public void AddToCart(Product p)
        {
            if (Cart == null || Cart.Items == null)
                return;

            // Create p As A New Product, So It Doesn't Interfere w/ the Inventory's Version
            p = new Product
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                MarkdownPrice = p.MarkdownPrice,
                Quantity = 1,
                IsBOGO = p.IsBOGO,
                IsMarkdown = p.IsMarkdown
            };

            // Check if Product is Available in Inventory
            var productInInv = InventoryServiceProxy.Current.Products?.FirstOrDefault(prod_in_Inv => prod_in_Inv.Id == p.Id);

            if (productInInv == null || productInInv.Quantity <= 0)
                return;

            // Decrement Quantity of Selected Product From Inventory
            productInInv.Quantity--;

            // Determine if We're Adding or Updating the Cart
            var productInCart = Cart.Items?.FirstOrDefault(prod_in_Cart => prod_in_Cart.Id == p.Id);

            if (productInCart == null)
                // Add the Product Into the Cart
                Cart.Items?.Add(p);
            else
                // Update the Quantity In the Cart
                productInCart.Quantity++;
        }
        public void RemoveFromCart(Product p)
        {
            if (Cart == null)
                return;

            // Find Product in Cart to Delete
            var productToDelete = Cart.Items?.FirstOrDefault(prod_In_Cart => prod_In_Cart.Id == p.Id);

            // Remove
            if (productToDelete != null)
                productToDelete.Quantity--;

            if(productToDelete?.Quantity == 0)
                Cart.Items?.Remove(productToDelete);

            // Find Product in Inventory to Add Back
            var addtoInv = InventoryServiceProxy.Current.Products?.FirstOrDefault(prod_In_Inv => prod_In_Inv.Id == p.Id);

            if (addtoInv != null)
                addtoInv.Quantity++;    
        }

        public void ClearCart()
        {
            if (Cart != null && Cart.Items != null)
                Cart.Items.Clear();
        }
    }
}
