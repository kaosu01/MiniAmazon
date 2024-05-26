using MiniAmazon.Library.Models;
using MiniAmazon.Library.Services;

namespace MiniAmazon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var productSvc = ProductServiceProxy.Current;
            var shoppingCart = new List<Product>();

            productSvc.AddorUpdate(
            new Product
            {
                Name = "Chair",
                Description = "You sit on it",
                Price = 19.99,
                Count = 3
            });

            productSvc.AddorUpdate(
            new Product
            {
                Name = "Eraser",
                Description = "You erase with it",
                Price = 4.99,
                Count = 7
            });

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
                            Inventory(productSvc);
                            break;
                        }
                    case "S":
                        {
                            Shopping(productSvc, shoppingCart);
                            break;
                        }
                    case "C":
                        {
                            if (Checkout(shoppingCart))
                                Console.WriteLine("Thank You for Shopping at Amazon");
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
        static void Inventory(ProductServiceProxy productSvc)
        {
            bool inInventory = true;
            string? invinput = null;

            while (inInventory)
            {
                Console.WriteLine("\nSelect An Option:");
                Console.WriteLine("(A) Add A Product\n(S) Display All Products");
                Console.WriteLine("(U) Update a Product\n(D) Delete a Product\n(L) Go Back to Main Menu");
                Console.Write("Insert Selection Here -> ");
                try { invinput = Console.ReadLine(); }
                catch (Exception) { }
                switch (invinput?.ToUpper())
                {
                    case "A":
                    {
                        string? pName = null;
                        string? pDescription = null;
                        double? pPrice = 0;
                        int pCount = 0;
                        Console.Write("Enter the Name of the Product: ");
                        pName = Console.ReadLine();
                        Console.Write("Enter the Description: ");
                        pDescription = Console.ReadLine();
                        Console.Write("Enter the Price: ");
                        pPrice = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Enter the Amount In Stock: ");
                        pCount = Convert.ToInt32(Console.ReadLine());

                        var product = new Product
                        {
                            Name = pName,
                            Description = pDescription,
                            Price = pPrice,
                            Count = pCount
                        };

                        product = productSvc?.AddorUpdate(product);

                        Console.WriteLine($"{product?.Name} has Been Added to the Inventory");

                        break;
                    }
                    case "S":
                    {
                        Console.WriteLine("\nInventory:");
                        productSvc?.Products?.ToList().ForEach(Console.WriteLine);
                        break;
                    }
                    case "U":
                    {
                        // Get the Item the User Wants to Update From List of Products
                        // Do the Same Thing As In Add

                        // Get Input for Searching Product to Update
                        int idInput = 0;
                        Product? product = null;

                        // Get Input for Updating Product
                        string? uName = null;
                        string? uDescription = null;
                        double? uPrice = 0;
                        int uCount = 0;

                        // Print Inventory Again to Show IDs
                        productSvc?.Products?.ToList().ForEach(Console.WriteLine);

                        // Get ID of the Product
                        do
                        {
                            Console.Write("Select Product by ID -> ");
                            idInput = Convert.ToInt32(Console.ReadLine());
                            product = productSvc?.Products?.FirstOrDefault(c => c.Id == idInput);

                            if (product == null)
                                Console.WriteLine("Invalid ID Selection... Please Try Again...");
                        } while (product == null);

                        // Getting Updated Information
                        Console.Write("Enter Updated Product Name -> ");
                        uName = Console.ReadLine();
                        Console.Write("Enter Updated Description -> ");
                        uDescription = Console.ReadLine();
                        Console.Write("Enter Updated Price -> ");
                        uPrice = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Enter Updated Quantity -> ");
                        uCount = Convert.ToInt32((Console.ReadLine()));

                        // Changing Data
                        product.Name = uName;
                        product.Description = uDescription;
                        product.Price = uPrice;
                        product.Count = uCount;

                        product = productSvc?.AddorUpdate(product);

                        Console.WriteLine("Product Has Been Updated");

                        break;
                    }
                    case "D":
                    {
                        // Get ID and Delete Product
                        int idInput = 0;
                        string? ans = null;
                        Product? product = null;

                        // Print Inventory Again to Show IDs
                        productSvc?.Products?.ToList().ForEach(Console.WriteLine);

                        // Get ID of the Product
                        do
                        {
                            Console.Write("Select Product by ID -> ");
                            idInput = Convert.ToInt32(Console.ReadLine());
                            product = productSvc?.Products?.FirstOrDefault(c => c.Id == idInput);

                            if (product == null)
                                Console.WriteLine("Invalid ID Selection... Please Try Again...");
                        } while (product == null);

                        Console.Write("Are You Sure You Want to Remove This Product From the Inventory (Y/N)? ");
                        ans = Console.ReadLine();

                        switch (ans?.ToUpper())
                        {
                            case "Y": { productSvc?.Delete(idInput); break; }
                            case "N": { break; }
                            default:
                                {
                                    Console.WriteLine("Invalid Selection... Please Try Again...");
                                    Console.Write("Are You Sure You Want to Remove This Product From the Inventory (Y/N)? ");
                                    ans = Console.ReadLine();
                                    break;
                                }
                        }

                        Console.WriteLine($"{product.Name} Has Been Removed From the Inventory");

                        break;
                    }
                    case "L":
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
        static void Shopping(ProductServiceProxy productSvc, List<Product> shoppingCart)
        {
            bool shopping = true;   // Continue Shopping Until Checkout
            string? shopInput = null;

            // Print Items on Sale
            Console.WriteLine("Inventory:");
            productSvc?.Products?.ToList().ForEach(Console.WriteLine);

            // Simulate Shopping
            while (shopping)
            {
                // Get Input
                Console.WriteLine("\nSelect An Option:");
                Console.WriteLine("(A) Add A Product to My Cart\n(D) Display All Items in My Cart");
                Console.WriteLine("(R) Remove a Product From My Cart\n(C) Checkout\n(L) Go Back to Main Menu");
                Console.Write("Insert Selection Here -> ");
                try { shopInput = Console.ReadLine(); }
                catch (Exception) { }
                switch (shopInput?.ToUpper())
                {
                    case "A":
                    {
                        // Variables Used to Add a Product to the User's Cart
                        int idInput = 0;
                        int pCount = 0;
                        Product? product = null;
                        Product? productToAdd = null;

                        // Get Product Using ID
                        do
                        {
                            productSvc?.Products?.ToList().ForEach(Console.WriteLine);
                            Console.Write("Enter the ProductID of the Product You Want to Add -> ");
                            idInput = Convert.ToInt32(Console.ReadLine());
                            product = productSvc?.Products?.FirstOrDefault(c => c.Id == idInput);

                            if (product == null)
                                Console.WriteLine("Invalid ID Selection... Please Try Again...");
                        } while (product == null);

                        do
                        {
                            Console.Write($"How Many of the Product \"{product.Name}\" Do You Want to Add? ");
                            pCount = Convert.ToInt32(Console.ReadLine());

                            if (pCount > product.Count)
                                Console.WriteLine("You Have Selected A Quantity Greater than Available... Please Try Again...");
                        } while (pCount > product.Count);

                        // Search If the Item User Wants to Add to Their Cart is Already in the Cart
                        // We'll Just Add the Count of the Item Rather than Make a New Product Object
                        var searchProduct = shoppingCart?.FirstOrDefault(c => c.Id == idInput);

                        // If Product is Already in the Cart
                        if (searchProduct != null)
                        {
                            searchProduct.Count += pCount;
                        }
                        // Make A Product Object to Add That to Shopping Cart
                        else
                        {
                            productToAdd = new Product
                            {
                                Id = product.Id,
                                Name = product.Name,
                                Description = product.Description,
                                Price = product.Price,
                                Count = pCount
                            };
                            shoppingCart?.Add(productToAdd);
                        }

                        product.Count -= pCount;
                        product = productSvc?.AddorUpdate(product);

                        Console.WriteLine($"{productToAdd?.Name} Has Been Added to Your Cart");

                        break;
                    }
                    case "D":
                    {
                        if (shoppingCart?.Count == 0)
                        {
                            Console.WriteLine("Shopping Cart is Currently Empty");
                        }
                        else
                        {
                            Console.WriteLine("My Cart:");
                            shoppingCart?.ToList().ForEach(Console.WriteLine);
                        }
                        break;
                    }
                    case "R":
                    {
                        int idInput = 0;
                        int rCount = 0;
                        Product? productToDelete = null;
                        Product? product = null;

                        // Get Product Using ID
                        do
                        {
                            shoppingCart?.ToList().ForEach(Console.WriteLine);
                            Console.Write("Enter the ProductID of the Product You Want to Remove -> ");
                            idInput = Convert.ToInt32(Console.ReadLine());
                            productToDelete = shoppingCart?.FirstOrDefault(c => c.Id == idInput);

                            if (productToDelete == null)
                                Console.WriteLine("Invalid ID Selection... Please Try Again...");
                        } while (productToDelete == null);

                        do
                        {
                            Console.Write($"How Many of the Product \"{productToDelete.Name}\" Do You Want to Remove? ");
                            rCount = Convert.ToInt32(Console.ReadLine());

                            if (rCount > productToDelete.Count)
                                Console.WriteLine("You Have Selected A Quantity Greater than Available... Please Try Again...");
                        } while (rCount > productToDelete.Count);

                        // Get the Product from Inventory, Add Back the Amount the User Removed From Their Cart
                        product = productSvc?.Products?.FirstOrDefault(c => c.Id == idInput);
                        product.Count += rCount;
                        productToDelete.Count -= rCount;
                        product = productSvc?.AddorUpdate(product);

                        if (productToDelete.Count == 0)
                        {
                            shoppingCart?.Remove(productToDelete);
                            Console.WriteLine($"Removed \"{productToDelete.Name}\" From Your Cart");
                        }
                        else
                        {
                            Console.WriteLine($"Removed {rCount} of the Product \"{productToDelete.Name}\" From Your Cart");
                        }

                        break;
                    }
                    case "C":
                    {
                        if (Checkout(shoppingCart))
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
        static bool Checkout(List<Product> shoppingCart)
        {
            if (shoppingCart.Count != 0)
            {
                double? subTotal = 0;
                double tax = 0.07;
                Console.WriteLine("Order:");
                for (int i = 0; i < shoppingCart.Count; i++)
                {
                    Console.WriteLine(shoppingCart[i]);
                    Product? product = shoppingCart[i];
                    subTotal += product.Price * product.Count;
                }
                string strSubTotal = string.Format("{0:0.00}", subTotal);
                subTotal = Convert.ToDouble(strSubTotal);
                double? withTax = subTotal * tax;
                string wTax = string.Format("{0:0.00}", withTax);
                withTax = Convert.ToDouble(wTax);
                double? total = subTotal + withTax;
                string strTotal = string.Format("{0:0.00}", total);
                total = Convert.ToDouble(strTotal);

                Console.WriteLine($"Subtotal:\t{subTotal}");
                Console.WriteLine($"Tax:\t\t{withTax}");
                Console.WriteLine($"Total:\t\t{total}");

                shoppingCart.Clear();

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