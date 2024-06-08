using MiniAmazon.MAUI.ViewModels;

namespace MiniAmazon.MAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        private void GoToInventory(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//InventoryManagement");
        }

        private void GoToShop(object sender, EventArgs e)
        {

        }
    }

}
