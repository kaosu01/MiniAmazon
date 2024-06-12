using MiniAmazon.Library.Models;
using MiniAmazon.Library.Services;
using System.Windows.Input;

namespace MiniAmazon.MAUI.ViewModels
{
    public class ProductViewModel
    {
        public ICommand? EditCommand { get; private set; }
        public void SetupCommands()
        {
            EditCommand = new Command((p) => ExecuteEdit(p as ProductViewModel));
        }
        private void ExecuteEdit(ProductViewModel? p)
        {
            if (p?.Product == null)
                return;
            Shell.Current.GoToAsync($"//Product?productId={p.Product.Id}");
        }

        public Product? Product;

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
        public decimal Price
        {
            get
            {
                return Product?.Price ?? 0;
            }
            set
            {
                if (Product != null)
                    Product.Price = value;
            }
        }
        public int Quantity
        {
            get
            {
                return Product?.Quantity ?? 0;
            }
            set
            {
                if (Product != null)
                    Product.Quantity = value;
            }
        }
        public int Id
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

        public ProductViewModel()
        {
            Product = new Product();
            SetupCommands();
        }

        public ProductViewModel(Product p)
        {
            Product = p;
            SetupCommands();
        }

        public ProductViewModel(int id)
        {
            Product = InventoryService.Current?.Products?.FirstOrDefault(p => p.Id == id);
            if (Product != null)
                Product = new Product();
            SetupCommands();
        }

        public void Add()
        {
            InventoryService.Current.AddorUpdate(Product);
        }

        public string? Display() { return ToString(); }

        
    }
}
