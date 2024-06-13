namespace MiniAmazon.MAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void GoToInventory(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Inventory");
        }

        private void GoToShop(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Shop");
        }
    }

}
