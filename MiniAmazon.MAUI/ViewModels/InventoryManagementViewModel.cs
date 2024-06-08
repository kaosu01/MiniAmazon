using MiniAmazon.Library.Models;
using MiniAmazon.Library.Services;

namespace MiniAmazon.MAUI.ViewModels
{
    
    public class InventoryManagementViewModel
    {
        public List<Product> Products
        {
            get
            {
                return InventoryService.Current.Products?.ToList() ?? new List<Product>();
            }
        }
        public InventoryManagementViewModel() { }
    }
}
