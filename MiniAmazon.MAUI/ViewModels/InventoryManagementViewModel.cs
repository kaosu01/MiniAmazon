using MiniAmazon.Library.Services;
using System.Net.Http.Headers;

namespace MiniAmazon.MAUI.ViewModels
{
    
    public class InventoryManagementViewModel
    {
        public List<ProductViewModel> Products
        {
            get
            {
                return InventoryService.Current.Products?.Select(p => new ProductViewModel(p)).ToList() 
                    ?? new List<ProductViewModel>();
            }
        }
        public InventoryManagementViewModel() { }
    }
}
