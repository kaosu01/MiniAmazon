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
    class ReceiptViewModel : INotifyPropertyChanged
    {
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
                if (ShoppingCartService.Current?.Cart?.Items?.Count == 0)
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
                    return $"${tax} (7%)";
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
                    decimal tax = Math.Round(Convert.ToDecimal(DisplayTax?.Replace("$", "").Replace("(7%)", "")), 2);
                    return $"${subTotal + tax}";
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        public void ClearCheckoutCart()
        {
            ShoppingCartService.Current.ClearCart();
        }
    }
}
