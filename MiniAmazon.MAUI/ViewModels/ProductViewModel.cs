using MiniAmazon.Library.Models;
using MiniAmazon.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAmazon.MAUI.ViewModels
{
    public class ProductViewModel
    {
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
                return $"{Product.Price:C}";
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
        }

        public ProductViewModel(Product? p)
        {
            if (p != null)
                Product = p;
            else
                Product = new Product();
        }

        public void Add()
        {
            if (Product != null)
                InventoryService.Current.AddorUpdate(Product);
        }
    }
}
