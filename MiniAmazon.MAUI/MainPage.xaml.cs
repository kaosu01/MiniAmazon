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
    }

}
