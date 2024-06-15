using MiniAmazon.Library.Models;
using System.Collections.ObjectModel;

namespace MiniAmazon.Library.Services 
{
    public class ShoppingCartService
    {
        private List<ShoppingCart> carts;
        private ShoppingCartService() { carts = new List<ShoppingCart>(); }
        private static ShoppingCartService? instance;
        private static object instanceLock = new object();
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
        public ReadOnlyCollection<ShoppingCart> Carts
        {
            get { return carts.AsReadOnly(); }
        }
        private int NextId
        {
            get
            {
                if (!carts.Any())
                    return 1;
                return carts.Select(c => c.Id).Max() + 1;
            }
        }
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
        public void DeleteCart(int id)
        {
            if (carts == null)
                return;

            // Find the Cart the User Wants to Delete
            var cartToDelete = carts.FirstOrDefault(c => c.Id == id);

            if(cartToDelete != null)
                carts.Remove(cartToDelete);
        }
        public void AddToCart(Product product, int id)
        {
            if (carts == null)
                return;

            // Find the Correct Cart to Add To
            var getCart = carts.FirstOrDefault(findCart => findCart.Id == id);

            if (getCart == null)
                return;

            // Check if Product is Available in Inventory
            var productInInv = InventoryService.Current.Products?.FirstOrDefault(prod_in_Inv => prod_in_Inv.Id == product.Id);

            if (productInInv == null)
                return;

            // Decrement Quantity of Selected Product From Inventory
            productInInv.Quantity -= product.Quantity;

            // Determine if We're Adding or Updating the Cart
            var productInCart = getCart.Items?.FirstOrDefault(prod_in_Cart => prod_in_Cart.Id == product.Id);

            if (productInCart == null)
                // Add the Product Into the Cart
                getCart.Items?.Add(product);
            else
                // Update the Quantity In the Cart
                productInCart.Quantity++;
        }
        public void RemoveFromCart(Product product, int cartId)
        {
            if (carts == null)
                return;

            var findCart = carts.FirstOrDefault(c => c.Id == cartId);

            if(findCart != null)
            {
                // Find Product in Cart to Delete
                var productToDelete = findCart.Items?.FirstOrDefault(prod_In_Inv => prod_In_Inv.Id == product.Id);

                // Remove
                if (productToDelete != null)
                    productToDelete.Quantity--;

                if(productToDelete?.Quantity == 0)
                    findCart.Items?.Remove(productToDelete);

                // Find Product in Inventory to Add Back
                var addtoInv = InventoryService.Current.Products?.FirstOrDefault(p => p.Id == product.Id);

                if (addtoInv != null)
                    addtoInv.Quantity++;

            }
        }
    }
}
