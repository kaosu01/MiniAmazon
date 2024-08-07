﻿using MiniAmazon.Library.Models;
using MiniAmazon.Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
            NotifyPropertyChanged(nameof(ShoppingProducts));
        }

        public void SearchInventory()
        {
            NotifyPropertyChanged(nameof(ShoppingProducts));
        }

        public void RefreshCheckoutCart()
        {
            NotifyPropertyChanged(nameof(CheckoutCartProducts));
            NotifyPropertyChanged(nameof(DisplayCartSubtotal));
        }

        public void RefreshWishlist()
        {
            NotifyPropertyChanged(nameof(WishlistProducts));
            NotifyPropertyChanged(nameof(DisplayWishlistSubtotal));
        }

        public ShopViewModel()
        {
            SearchInventoryQuery = string.Empty;
        }

        public ShoppingCart Cart
        {
            get
            {
                return ShoppingCartServiceProxy.Current.Carts[0];
            }
        }
        public ShoppingCart Wishlist
        {
            get
            {
                return ShoppingCartServiceProxy.Current.Carts[1];
            }
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
        public List<ProductViewModel> ShoppingProducts
        {
            get
            {
                return InventoryServiceProxy.Current?.Products?.Where(p => p != null)
                    .Where(p => p.Name?.ToUpper()?.Contains(SearchInventoryQuery.ToUpper()) ?? false)
                    .Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>();
            }
        }

        public List<ProductViewModel> CheckoutCartProducts
        {
            get
            {
                return ShoppingCartServiceProxy.Current?.Carts[0].Items?.Where(p => p != null)
                    .Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>();
            }
        }

        public List<ProductViewModel> WishlistProducts
        {
            get
            {
                return ShoppingCartServiceProxy.Current?.Carts[1].Items?.Where(p => p != null)
                    .Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>();
            }
        }

        public string? DisplayCartSubtotal
        {
            get
            {
                if (ShoppingCartServiceProxy.Current?.Carts[0]?.Items?.Count == 0)
                    return "$0.00";
                else
                {
                    decimal subTotal = 0;
                    var cartList = ShoppingCartServiceProxy.Current?.Carts[0].Items;
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

        public string? DisplayWishlistSubtotal
        {
            get
            {
                if (ShoppingCartServiceProxy.Current?.Carts[1]?.Items?.Count == 0)
                    return "$0.00";
                else
                {
                    decimal subTotal = 0;
                    var cartList = ShoppingCartServiceProxy.Current?.Carts[1].Items;
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

        public void GetReceipt(int id)
        {
            Shell.Current.GoToAsync($"//Receipt?cartId={id}");
        }
    }
}
