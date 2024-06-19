using MiniAmazon.MAUI.ViewModels;

namespace MiniAmazon.MAUI.Views;

public partial class ReceiptView : ContentPage
{
	public ReceiptView()
	{
		InitializeComponent();
        BindingContext = new ReceiptViewModel();
	}

    private void MainPage_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        (BindingContext as ReceiptViewModel)?.ClearCheckoutCart();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ReceiptViewModel)?.RefreshCheckoutCart();
        (BindingContext as ReceiptViewModel)?.RefreshCosts();
    }
}