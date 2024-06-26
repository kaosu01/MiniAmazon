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
        (BindingContext as ShopViewModel)?.RefreshCheckoutCart();
        (BindingContext as ShopViewModel)?.RefreshSubtotal();
    }

    private void SearchInventory_Clicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel)?.SearchInventory();
    }

    private void RefreshSearch_Clicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel)?.RefreshInventory();
    }

    private void InLineAddToCart_Clicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel)?.RefreshInventory();
        (BindingContext as ShopViewModel)?.RefreshCheckoutCart();
        (BindingContext as ShopViewModel)?.RefreshSubtotal();
    }

    private void InLineRemoveFromCart_Clicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel)?.RefreshInventory();
        (BindingContext as ShopViewModel)?.RefreshCheckoutCart();
        (BindingContext as ShopViewModel)?.RefreshSubtotal();
    }

    private void Checkout_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Receipt");
    }
}