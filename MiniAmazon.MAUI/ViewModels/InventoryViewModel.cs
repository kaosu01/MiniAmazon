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
    public class InventoryViewModel : INotifyPropertyChanged
    {
        public List<ProductViewModel> Products
        {
            get
            {
                return InventoryServiceProxy.Current?.Products?.Where(p => p != null)
                    .Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>();
            }
        } 

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public void RefreshInventory()
        {
            NotifyPropertyChanged(nameof(Products));
        }
    }
}
