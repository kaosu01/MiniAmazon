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
        public ICommand? EditCommand { get; private set; }

        private void ExecuteEdit(ProductViewModel? p)
        {
            if (p?.Product == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Product?productId={p.Product.Id}");
        }

        public void SetupCommands()
        {
            EditCommand = new Command(
               (p) => ExecuteEdit(p as ProductViewModel));
        }
        public Product? Product { get; set; }

        public int Id
        {
            get
            {
                return Product?.Id ?? 0;
            }
            set
            {
                if (Product != null)
                {
                    Product.Id = value;
                }
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

        public string? Price
        {
            get
            {
                if (Product == null)
                    return string.Empty;
                return $"${Product.Price}";
            }
            set
            {
                if (Product == null)
                    return;
                if (decimal.TryParse(value, out var price))
                    Product.Price = price;
            }
        }

        public string? Quantity
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
            {
                Product = new Product();
            }
        }

        public void Add()
        {
            if (Product != null)
                InventoryService.Current.AddorUpdate(Product);
        }
    }
}
