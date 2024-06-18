using MiniAmazon.Library.Models;
using MiniAmazon.Library.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniAmazon.MAUI.ViewModels
{
    public class ProductViewModel
    {
        public ICommand? AddToCartCommand { get; private set; }
        public ICommand? EditCommand { get; private set; }
        public ICommand? RemoveCommand { get; private set; }
        public ICommand? RemoveFromCartCommand { get; private set; }

        private void ExecuteEdit(ProductViewModel? p)
        {
            if (p?.Product == null)
                return;
            Shell.Current.GoToAsync($"//Product?productId={p.Product.Id}");
        }

        private void ExecuteRemove(int? id)
        {
            if (id == null)
                return;
            InventoryService.Current.Remove(id ?? 0);
        }

        private void ExecuteAddToCart(Product? p)
        {
            if (p != null)
                ShoppingCartService.Current.AddToCart(p);
        }

        private void ExecuteRemoveFromCart(Product? p)
        {
            if (p != null)
                ShoppingCartService.Current.RemoveFromCart(p);
        }

        public void SetupCommands()
        {
            EditCommand = new Command(
               (p) => ExecuteEdit(p as ProductViewModel));
            RemoveCommand = new Command(
                (p) => ExecuteRemove((p as ProductViewModel)?.Product?.Id));
            AddToCartCommand = new Command(
                (p) => ExecuteAddToCart((p as ProductViewModel)?.Product));
            RemoveFromCartCommand = new Command(
                (p) => ExecuteRemoveFromCart((p as ProductViewModel)?.Product));
        }
        public Product? Product { get; set; }

        public int PId
        {
            get
            {
                return Product?.Id ?? 0;
            }
            set
            {
                if (Product != null)
                    Product.Id = value;
            }
        }

        public string? Name
        {
            get
            {
                return Product?.Name ?? string.Empty;
            }
            set
            {
                if (Product != null)
                    Product.Name = value;
            }
        }

        public string? Description
        {
            get
            {
                return Product?.Description ?? string.Empty;
            }
            set
            {
                if (Product != null)
                    Product.Description = value;
            }
        }

        public string? TotalProductPrice
        {
            get
            {
                if (Product == null)
                    return string.Empty;
                return $"${Product.Price * Product.Quantity}";
            }
        }

        public string? DisplayPrice
        {
            get
            {
                if (Product == null)
                    return string.Empty;
                return $"${Product.Price}";
            }
        }

        public string? EditingPrice
        {
            get
            {
                if (Product == null)
                    return string.Empty;
                return DisplayPrice?.Replace("$", "");
            }
            set
            {
                if (Product == null)
                    return;
                if (decimal.TryParse(value, out var price))
                    Product.Price = price;
            }
        }

        public string? DisplayQuantity
        {
            get
            {
                if (Product == null)
                    return string.Empty;
                else if (Product.Quantity == 0)
                    return "Out of Stock";
                return $"{Product.Quantity}";
            }
            set
            {
                if (Product == null)
                    return;
                if (int.TryParse(value, out var quantity))
                    Product.Quantity = quantity;
            }
        }

        public string? EditingQuantity
        {
            get
            {
                if (Product == null)
                    return string.Empty;
                return $"{Product.Quantity}";
            }
            set
            {
                if (Product == null)
                    return;
                if (int.TryParse(value, out var quantity))
                    Product.Quantity = quantity;
            }
        }

        public ShoppingCart? CheckoutCart { get; set; }

        public ProductViewModel()
        {
            Product = new Product();
            SetupCommands();
        }

        public ProductViewModel(Product? p)
        {
            if (p != null)
                Product = p;
            else
                Product = new Product();
            SetupCommands();
        }

        public ProductViewModel(int id)
        {
            Product = InventoryService.Current?.Products?.FirstOrDefault(p => p.Id == id);
            if (Product == null)
                Product = new Product();
        }

        public void Add()
        {
            if (Product != null)
                InventoryService.Current.AddorUpdate(Product);
        }
    }
}
