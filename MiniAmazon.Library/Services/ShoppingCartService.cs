using MiniAmazon.Library.Models;
using System.Collections.ObjectModel;

namespace MiniAmazon.Library.Services 
{
    public class ShoppingCartService
    {
        private ShoppingCartService()
        {
            Cart = new ShoppingCart
            {
                Items = new List<Product>()
                /*{
                    new Product
                    {
                        Id = 1,
                        Name = "Chair",
                        Description = "You sit on it",
                        Price = 19.99m,
                        Quantity = 2
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Eraser",
                        Description = "You erase with it",
                        Price = 4.99m,
                        Quantity = 4
                    }
                }*/
            };
        }

        private static ShoppingCartService? instance;
        private static object instanceLock = new object();

        public ShoppingCart Cart { get; private set; }

        public static ShoppingCartService Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartService();
                    }
                }
                return instance;
            }
        }

        public ReadOnlyCollection<ShoppingCart> carts;

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
                Quantity = 1
            };

            // Check if Product is Available in Inventory
            var productInInv = InventoryService.Current.Products?.FirstOrDefault(prod_in_Inv => prod_in_Inv.Id == p.Id);

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
            var addtoInv = InventoryService.Current.Products?.FirstOrDefault(prod_In_Inv => prod_In_Inv.Id == p.Id);

            if (addtoInv != null)
                addtoInv.Quantity++;    
        }
    }
}
