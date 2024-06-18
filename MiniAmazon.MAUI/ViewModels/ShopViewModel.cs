using MiniAmazon.Library.Models;
using MiniAmazon.Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniAmazon.MAUI.ViewModels
{
    public class ShopViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshInventory()
        {
            SearchInventoryQuery = string.Empty;
            NotifyPropertyChanged(nameof(Products));
        }

        public void SearchInventory()
        {
            NotifyPropertyChanged(nameof(Products));
        }

        public void RefreshCheckoutCart()
        {
            NotifyPropertyChanged(nameof(CheckoutCartProducts));
        }

        public void RefreshCosts()
        {
            NotifyPropertyChanged(nameof(DisplaySubtotal));
            NotifyPropertyChanged(nameof(DisplayTax));
            NotifyPropertyChanged(nameof(DisplayTotal));
        }

        public ShopViewModel()
        {
            SearchInventoryQuery = string.Empty;
        }

        private string searchInventoryQuery;

        public string SearchInventoryQuery
        {
            set
            {
                searchInventoryQuery = value;
                NotifyPropertyChanged();
            }
            get { return searchInventoryQuery; }
        }
        public List<ProductViewModel> Products
        {
            get
            {
                return InventoryService.Current?.Products?.Where(p => p != null)
                    .Where(p => p.Name?.ToUpper()?.Contains(SearchInventoryQuery.ToUpper()) ?? false)
                    .Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>();
            }
        }

        public List<ProductViewModel> CheckoutCartProducts
        {
            get
            {
                return ShoppingCartService.Current?.Cart.Items?.Where(p => p != null)
                    .Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>();
            }
        }

        public string? DisplaySubtotal
        {
            get
            {
                if (ShoppingCartService.Current?.Cart.Items == null)
                    return "$0.00";
                else
                {
                    decimal subTotal = 0;
                    var cartList = ShoppingCartService.Current?.Cart.Items;
                    for (int i = 0; i < cartList?.Count; i++)
                    {
                        subTotal += cartList[i].Price * cartList[i].Quantity;
                    }
                    return $"${subTotal}";
                }
            }
        }

        public string? DisplayTax
        {
            get
            {
                if (DisplaySubtotal == "$0.00")
                    return "$0.00";
                else
                {
                    decimal tax = Convert.ToDecimal(DisplaySubtotal?.Replace("$", ""));
                    tax = Math.Round(tax * 0.07m, 2);
                    return $"${tax}";
                }
            }
        }

        public string? DisplayTotal
        {
            get
            {
                if (DisplaySubtotal == "$0.00")
                    return "$0.00";
                else
                {
                    decimal subTotal = Math.Round(Convert.ToDecimal(DisplaySubtotal?.Replace("$", "")), 2);
                    decimal tax = Math.Round(Convert.ToDecimal(DisplayTax?.Replace("$", "")), 2);
                    return $"${subTotal + tax}";
                }
            }
        }
    }
}
