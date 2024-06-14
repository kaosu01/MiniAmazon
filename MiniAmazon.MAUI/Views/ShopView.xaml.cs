using MiniAmazon.MAUI.ViewModels;

namespace MiniAmazon.MAUI.Views;

public partial class ShopView : ContentPage
{
	public ShopView()
	{
		InitializeComponent();
		BindingContext = new ShopViewModel();
	}

    private void Logo_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ShopViewModel)?.RefreshInventory();
    }

    private void SearchInventory_Clicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel)?.SearchInventory();
    }

    private void RefreshSearch_Clicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel)?.RefreshInventory();
    }
}