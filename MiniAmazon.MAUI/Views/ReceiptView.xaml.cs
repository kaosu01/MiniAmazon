using MiniAmazon.Library.Models;
using MiniAmazon.MAUI.ViewModels;

namespace MiniAmazon.MAUI.Views;

[QueryProperty(nameof(CartId), "cartId")]
public partial class ReceiptView : ContentPage
{
    public int CartId { get; set; }
	public ReceiptView()
	{
		InitializeComponent();
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
        BindingContext = new ReceiptViewModel(CartId);
        (BindingContext as ReceiptViewModel)?.RefreshCheckoutCart();
        (BindingContext as ReceiptViewModel)?.RefreshCosts();
    }
}