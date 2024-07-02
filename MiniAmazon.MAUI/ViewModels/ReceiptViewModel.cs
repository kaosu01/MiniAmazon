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
                return ShoppingCartServiceProxy.Current?.Cart.Items?.Where(p => p != null)
                    .Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>();
            }
        }
        public string? DisplaySubtotal
        {
            get
            {
                if (ShoppingCartServiceProxy.Current?.Cart?.Items?.Count == 0)
                    return "$0.00";
                else
                {
                    decimal subTotal = 0;
                    var cartList = ShoppingCartServiceProxy.Current?.Cart.Items;
                    for (int i = 0; i < cartList?.Count; i++)
                    {
                        if (cartList[i].IsMarkdown && cartList[i].IsBOGO)
                            subTotal += (cartList[i].Quantity % 2 * cartList[i].MarkdownPrice) + (cartList[i].Quantity / 2 * cartList[i].MarkdownPrice);
                        else if (cartList[i].IsMarkdown)
                            subTotal += cartList[i].MarkdownPrice * cartList[i].Quantity;
                        else if (cartList[i].IsBOGO)
                            subTotal += (cartList[i].Quantity % 2 * cartList[i].Price) + (cartList[i].Quantity / 2 * cartList[i].Price);
                        else
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
                    tax = Math.Round(Convert.ToDecimal(tax * Convert.ToDecimal(SelectedTaxRate?.Replace("%", "")) / 100), 2);
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
                    decimal tax = Math.Round(Convert.ToDecimal(DisplayTax?.Split(' ')[0].Replace("$", "")), 2);
                    return $"${subTotal + tax}";
                }
            }
        }

        private string? selectedTaxRate;

        public string? SelectedTaxRate
        {
            get
            {
                return selectedTaxRate ?? "7.0%";
            }
            set
            {
                selectedTaxRate = value?.Replace("%", "");
                NotifyPropertyChanged(nameof(DisplayTax));
                NotifyPropertyChanged(nameof(DisplayTotal));
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
            ShoppingCartServiceProxy.Current.ClearCart();
        }

        public List<string> TaxRates
        {
            get
            {
                return Enumerable.Range(0, (int)((10m - 0) / 0.1m) + 1)
                         .Select(i => 0 + i * 0.1m)
                         .Select(x => $"{Math.Round(x, 1)}%")
                         .ToList();
            }
        }
    }
}
