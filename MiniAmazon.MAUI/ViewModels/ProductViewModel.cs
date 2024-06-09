using MiniAmazon.Library.Models;
using System.Windows.Input;

namespace MiniAmazon.MAUI.ViewModels
{
    public class ProductViewModel
    {
        public ICommand EditCommand { get; set; }
        public void SetupCommands()
        {
            EditCommand = new Command((p) => ExecuteEdit(p as ProductViewModel));
        }
        private void ExecuteEdit(ProductViewModel? p)
        {
            if (p.Product == null)
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
                {
                    Product.Name = value;
                }
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

        public string? Display() { return ToString(); }

        
    }
}
