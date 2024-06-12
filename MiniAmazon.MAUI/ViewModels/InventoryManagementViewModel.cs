using MiniAmazon.Library.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MiniAmazon.MAUI.ViewModels
{
    
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<ProductViewModel> Products
        {
            get
            {
                return InventoryService.Current.Products?.Select(p => new ProductViewModel(p)).ToList() 
                    ?? new List<ProductViewModel>();
            }
        }

        public ProductViewModel SelectedProduct { get; set; }

        public InventoryManagementViewModel() { }

        public void RefreshProducts()
        {
            NotifyPropertyChanged("Products");
        }
    }
}
