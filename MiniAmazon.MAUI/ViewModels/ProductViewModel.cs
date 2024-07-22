using MiniAmazon.Library.DTOs;
using MiniAmazon.Library.Models;
using MiniAmazon.Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniAmazon.MAUI.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
{
        public ICommand? AddToCartCommand { get; private set; }
        public ICommand? RemoveFromCartCommand { get; private set; }
        public ICommand? EditCommand { get; private set; }
        public ICommand? RemoveCommand { get; private set; }
        public ICommand? AddToWishlistCommand { get; private set; }
        public ICommand? RemoveFromWishlistCommand { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(ButtonsVisibility));
            NotifyPropertyChanged(nameof(SaleAndButtons));
        }

        public Visibility ButtonsVisibility
        {
            get
            {
                if (Product == null)
                    return Visibility.Visible;
                return Product.IsMarkdown ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility SaleAndButtons
        {
            get
            {
                if (Product == null)
                    return Visibility.Visible;
                return Product.IsMarkdown ? Visibility.Collapsed : Visibility.Visible;
            }
        }

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
            InventoryServiceProxy.Current.Remove(id ?? 0);
        }

        private void ExecuteAddToCart(ProductDTO? p)
        {
            if (p != null)
                ShoppingCartServiceProxy.Current.AddToCart(p, 0);
        }

        private void ExecuteRemoveFromCart(ProductDTO? p)
        {
            if (p != null)
                ShoppingCartServiceProxy.Current.RemoveFromCart(p, 0);
        }

        private void ExecuteAddToWishlist(ProductDTO? p)
        {
            if (p != null)
                ShoppingCartServiceProxy.Current.AddToCart(p, 1);
        }

        private void ExecuteRemoveFromWishlist(ProductDTO? p)
        {
            if (p != null)
                ShoppingCartServiceProxy.Current.RemoveFromCart(p, 1);
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
            AddToWishlistCommand = new Command(
                (p) => ExecuteAddToWishlist((p as ProductViewModel)?.Product));
            RemoveFromWishlistCommand = new Command(
                (p) => ExecuteRemoveFromWishlist((p as ProductViewModel)?.Product));
        }
        public ProductDTO? Product { get; set; }

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
                if (Product.IsMarkdown && Product.IsBOGO)
                    return $"${((Product.Quantity % 2) * Product.MarkdownPrice) + ((Product.Quantity / 2) * Product.MarkdownPrice)}";
                else if (Product.IsMarkdown)
                    return $"${Product.MarkdownPrice * Product.Quantity}";
                else if (Product.IsBOGO)
                    return $"${((Product.Quantity % 2) * Product.Price) + ((Product.Quantity / 2) * Product.Price)}";
                else
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

        public string? DisplaySalePrice
        {
            get
            {
                if (Product == null)
                    return string.Empty;
                if (Product.IsMarkdown == true)
                    return $"${Product.MarkdownPrice}";
                else
                    return string.Empty;
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

        public string? EditingSalePrice
        {
            get
            {
                if (Product == null)
                    return string.Empty;
                return DisplaySalePrice?.Replace("$", "");
            }
            set
            {
                if (Product == null)
                    return;
                if (decimal.TryParse(value, out var price))
                    Product.MarkdownPrice = price;
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

        public string? DisplayBOGOFree
        {
            get
            {
                if (Product == null)
                    return string.Empty;
                if (Product.IsBOGO == true)
                    return "BUY 1 GET 1\n      FREE!!!";
                return string.Empty;
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

        public bool MarkdownStrikethrough
        {
            get
            {
                if (Product == null)
                    return false;
                return Product.IsMarkdown;
            }
        }

        public bool EditingMarkdownBool
        {
            get
            {
                if (Product == null)
                    return false;
                return Product.IsMarkdown;
            }
            set
            {
                if (Product == null)
                    return;
                Product.IsMarkdown = value;
            }
        }

        public bool EditingBOGOFreeBool
        {
            get
            {
                if (Product == null)
                    return false;
                return Product.IsBOGO;
            }
            set
            {
                if (Product == null)
                    return;
                Product.IsBOGO = value;
            }
        }

        public ProductViewModel()
        {
            Product = new ProductDTO();
            SetupCommands();
        }

        public ProductViewModel(ProductDTO? p)
        {
            if (p != null)
                Product = p;
            else
                Product = new ProductDTO();
            SetupCommands();
        }

        public ProductViewModel(int id)
        {
            Product = InventoryServiceProxy.Current?.Products?.FirstOrDefault(p => p.Id == id);
            if (Product == null)
                Product = new ProductDTO();
        }

        public async void Add()
        {
            if (Product != null)
                Product = await InventoryServiceProxy.Current.AddorUpdate(Product);
        }
    }
}
