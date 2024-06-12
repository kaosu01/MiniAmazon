using MiniAmazon.Library.Models;
using MiniAmazon.Library.Services;
using System.ComponentModel.DataAnnotations;

namespace MiniAmazon.CLI
{
    internal class Program
    {
        static void Main()
        {
            var inventorySvc = InventoryService.Current;
            var shoppingCartSvc = ShoppingCartService.Current;

            // Create A Shopping Cart Used For Checkout
            var checkout_Cart = shoppingCartSvc.AddOrUpdate(new ShoppingCart());

            // Variables for Simulation of Shopping
            string? menuinput = null;
            bool shopping = true;

            // Simulation of Shopping
            while (shopping)
            {
                // Print Statements for Menu
                Console.WriteLine("\nWelcome to Amazon\nSelect From the Menu Below:");
                Console.WriteLine("(I) Inventory\n(S) Shop\n(C) Checkout\n(E) Exit");
                Console.Write("Insert Selection Here -> ");
                try { menuinput = Console.ReadLine(); }
                catch (Exception) { }
                switch (menuinput?.ToUpper())
                {
                    case "I":
                        {
                            Inventory(inventorySvc);
                            break;
                        }
                    case "S":
                        {
                            Shopping(inventorySvc, shoppingCartSvc, checkout_Cart);
                            break;
                        }
                    case "C":
                        {
                            if (Checkout(checkout_Cart))
                                Console.WriteLine("Thank You for Shopping at Amazon!");
                            break;
                        }
                    case "E":
                        {
                            Console.WriteLine("Thank You for Shopping at Amazon!");
                            shopping = false;
                            break;
                        }
                    default: { Console.WriteLine("Invalid Choice... Please Try Again..."); break; }
                }
            }
        }

        static void Inventory(InventoryService inventorySvc)
        {
            bool inInventory = true;
            string? invinput = null;

            while (inInventory)
            {
                Console.WriteLine("\nSelect An Option:");
                Console.WriteLine("(A) Add A Product\n(S) Display All Products");
                Console.WriteLine("(U) Update A Product\n(D) Delete A Product\n(L) Go Back To Main Menu");
                Console.Write("Insert Selection Here -> ");
                try { invinput = Console.ReadLine(); }
                catch (Exception) { }
                switch (invinput?.ToUpper())
                {
                    case "A":   // Add
                        {
                            string? pName = null;
                            string? pDescription = null;
                            decimal pPrice = 0;
                            int pCount = 0;
                            Console.Write("Enter the Name of the Product: ");
                            pName = Console.ReadLine();
                            Console.Write("Enter the Description: ");
                            pDescription = Console.ReadLine();
                            Console.Write("Enter the Price: ");
                            pPrice = Convert.ToDecimal(Console.ReadLine());
                            Console.Write("Enter the Amount In Stock: ");
                            pCount = Convert.ToInt32(Console.ReadLine());

                            var product = new Product
                            {
                                Name = pName,
                                Description = pDescription,
                                Price = pPrice,
                                Quantity = pCount
                            };

                            product = inventorySvc?.AddorUpdate(product);

                            Console.WriteLine($"{product?.Name} has Been Added to the Inventory");

                            break;
                        }
                    case "S":   // Display
                        {
                            Console.WriteLine("\nInventory:");
                            inventorySvc?.Products?.ToList().ForEach(Console.WriteLine);
                            break;
                        }
                    case "U":   // Update
                        {
                            // Get Input for Searching Product to Update
                            int idInput = 0;
                            Product? product = null;

                            // Get Input for Updating Product
                            string? uName = null;
                            string? uDescription = null;
                            decimal uPrice = 0;
                            int uCount = 0;

                            // Print Inventory Again to Show IDs
                            inventorySvc?.Products?.ToList().ForEach(Console.WriteLine);

                            // Get ID of the Product
                            do
                            {
                                Console.Write("Select Product by ID -> ");
                                idInput = Convert.ToInt32(Console.ReadLine());
                                product = inventorySvc?.Products?.FirstOrDefault(c => c.Id == idInput);

                                if (product == null)
                                    Console.WriteLine("Invalid ID Selection... Please Try Again...");
                            } while (product == null);

                            // Getting Updated Information
                            Console.Write("Enter Updated Product Name -> ");
                            uName = Console.ReadLine();
                            Console.Write("Enter Updated Description -> ");
                            uDescription = Console.ReadLine();
                            Console.Write("Enter Updated Price -> ");
                            uPrice = Convert.ToDecimal(Console.ReadLine());
                            Console.Write("Enter Updated Quantity -> ");
                            uCount = Convert.ToInt32((Console.ReadLine()));

                            // Changing Data
                            product.Name = uName;
                            product.Description = uDescription;
                            product.Price = uPrice;
                            product.Quantity = uCount;

                            product = inventorySvc?.AddorUpdate(product);

                            Console.WriteLine("Product Has Been Updated");

                            break;
                        }
                    case "D":   // Remove
                        {
                            // Get ID and Delete Product
                            int idInput = 0;
                            string? ans = null;
                            Product? product = null;

                            // Print Inventory Again to Show IDs
                            inventorySvc?.Products?.ToList().ForEach(Console.WriteLine);

                            // Get ID of the Product
                            do
                            {
                                Console.Write("Select Product by ID -> ");
                                idInput = Convert.ToInt32(Console.ReadLine());
                                product = inventorySvc?.Products?.FirstOrDefault(c => c.Id == idInput);

                                if (product == null)
                                    Console.WriteLine("Invalid ID Selection... Please Try Again...");
                            } while (product == null);

                            Console.Write("Are You Sure You Want to Remove This Product From the Inventory (Y/N)? ");
                            ans = Console.ReadLine();

                            switch (ans?.ToUpper())
                            {
                                case "Y":
                                    {
                                        inventorySvc?.Remove(idInput);
                                        Console.WriteLine($"{product.Name} Has Been Removed From the Inventory");
                                        break;
                                    }
                                case "N":
                                    {
                                        Console.WriteLine($"{product.Name} Was Not Removed From the Inventory");
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("Invalid Selection... Please Try Again...");
                                        Console.Write("Are You Sure You Want to Remove This Product From the Inventory (Y/N)? ");
                                        ans = Console.ReadLine();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "L":   // Go Back to Main Menu
                        {
                            Console.WriteLine("Returning to Main Menu...");
                            inInventory = false;
                            break;
                        }
                    default: { Console.WriteLine("Invalid Choice... Please Try Again..."); break; }
                }
            }
            return;
        }
        static void Shopping(InventoryService inventorySvc, ShoppingCartService shoppingCartSvc, ShoppingCart checkout_Cart)
        {
            bool shopping = true;   // Continue Shopping Until Checkout
            string? shopInput = null;

            // Print Items on Sale
            Console.WriteLine("Inventory:");
            inventorySvc?.Products?.ToList().ForEach(Console.WriteLine);

            // Simulate Shopping
            while (shopping)
            {
                // Get Input
                Console.WriteLine("Select An Option:");
                Console.WriteLine("(A) Add A Product to My Cart\n(D) Display All Items In My Cart");
                Console.WriteLine("(R) Remove A Product From My Cart\n(C) Checkout\n(L) Go Back To Main Menu");
                Console.Write("Insert Selection Here -> ");
                try { shopInput = Console.ReadLine(); }
                catch (Exception) { }
                switch (shopInput?.ToUpper())
                {
                    case "A":   // Add
                        {
                            // Variables Used to Add a Product to the User's Cart
                            int idInput = 0;
                            int pCount = 0;
                            Product? product = null;
                            Product? productToAdd = null;

                            // Get Product Using ID
                            do
                            {
                                inventorySvc?.Products?.ToList().ForEach(Console.WriteLine);
                                Console.Write("Enter the ProductID of the Product You Want to Add -> ");
                                idInput = Convert.ToInt32(Console.ReadLine());
                                product = inventorySvc?.Products?.FirstOrDefault(c => c.Id == idInput);

                                if (product == null)
                                    Console.WriteLine("Invalid ID Selection... Please Try Again...");
                            } while (product == null);

                            do
                            {
                                Console.Write($"How Many of the Product \"{product.Name}\" Do You Want to Add? ");
                                pCount = Convert.ToInt32(Console.ReadLine());

                                if (pCount > product.Quantity)
                                    Console.WriteLine("You Have Selected A Quantity Greater than Available... Please Try Again...");
                            } while (pCount > product.Quantity);

                            productToAdd = new Product
                            {
                                Id = product.Id,
                                Name = product.Name,
                                Description = product.Description,
                                Price = product.Price,
                                Quantity = pCount
                            };
                            shoppingCartSvc?.AddToCart(productToAdd, checkout_Cart.Id);
                            Console.WriteLine($"{productToAdd.Name} Has Been Added to Your Cart");

                            break;
                        }
                    case "D":   // Display
                        {
                            if (checkout_Cart.Items?.Count == 0)
                            {
                                Console.WriteLine("Shopping Cart is Currently Empty");
                            }
                            else
                            {
                                Console.WriteLine("My Cart:");
                                checkout_Cart.Items?.ToList().ForEach(Console.WriteLine);
                            }
                            break;
                        }
                    case "R":   // Remove
                        {
                            int idInput = 0;
                            int rCount = 0;
                            int amountDeleted = 0;
                            Product? productToDelete = null;

                            // Get Product Using ID
                            do
                            {
                                checkout_Cart.Items?.ToList().ForEach(Console.WriteLine);
                                Console.Write("Enter the ProductID of the Product You Want to Remove -> ");
                                idInput = Convert.ToInt32(Console.ReadLine());
                                productToDelete = checkout_Cart.Items?.FirstOrDefault(c => c.Id == idInput);

                                if (productToDelete == null)
                                    Console.WriteLine("Invalid ID Selection... Please Try Again...");
                            } while (productToDelete == null);

                            // Get Quantity User Wants to Remove From Their Cart
                            do
                            {
                                Console.Write($"How Many of the Product \"{productToDelete.Name}\" Do You Want to Remove? ");
                                rCount = Convert.ToInt32(Console.ReadLine());

                                if (rCount > productToDelete.Quantity)
                                    Console.WriteLine("You Have Selected A Quantity Greater Than What's In Your Cart... Please Try Again...");
                            } while (rCount > productToDelete.Quantity);

                            amountDeleted = productToDelete.Quantity - rCount;

                            // Change the Quantity to How Much the User Wants to Remove From Their Cart, Then Remove
                            productToDelete = new Product()
                            {
                                Id = productToDelete.Id,
                                Name = productToDelete.Name,
                                Description = productToDelete.Description,
                                Price = productToDelete.Price,
                                Quantity = rCount
                            };

                            shoppingCartSvc?.RemoveFromCart(productToDelete, checkout_Cart.Id);

                            // Print Out Confirmation Message
                            if (amountDeleted == 0)
                                Console.WriteLine($"Removed \"{productToDelete.Name}\" From Your Cart");
                            else
                                Console.WriteLine($"Removed {rCount} of the Product \"{productToDelete.Name}\" From Your Cart");

                            break;
                        }
                    case "C":   // Checkout
                        {
                            if (Checkout(checkout_Cart))
                            {
                                shopping = false;
                                Console.WriteLine("Thank You for Shopping at Amazon");
                                Console.WriteLine("Returning Back to Main Menu...");
                            }
                            break;
                        }
                    case "L":
                        {
                            Console.WriteLine("Going Back to Main Menu...");
                            shopping = false;
                            break;
                        }
                    default: { Console.WriteLine("Invalid Choice... Please Try Again..."); break; }
                }
            }
        }
        static bool Checkout(ShoppingCart checkout_Cart)
        {
            if (checkout_Cart.Items?.Count != 0)
            {
                decimal subTotal = 0;
                decimal tax = 0.07m;
                decimal total;
                Console.WriteLine("Order:");
                if (checkout_Cart.Items != null)
                {
                    for (int i = 0; i < checkout_Cart.Items?.Count; i++)
                    {
                        Console.WriteLine(checkout_Cart.Items[i]);
                        Product product = checkout_Cart.Items[i];
                        subTotal += product.Price * product.Quantity;
                    }
                }
                total = subTotal + (subTotal * tax);

                Console.WriteLine($"Subtotal:\t${subTotal.ToString("0.00")}");
                Console.WriteLine($"Tax:\t\t${(subTotal * tax).ToString("0.00")}");
                Console.WriteLine($"Total:\t\t${total.ToString("0.00")}");

                checkout_Cart.Items?.Clear();

                return true;
            }
            else
            {
                Console.WriteLine("Nothing to Checkout...\nPlease Add Items to Your Cart Before Checking Out...");
                return false;
            }
        }
    }
}