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
    }
}
