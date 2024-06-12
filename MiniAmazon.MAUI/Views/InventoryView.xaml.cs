using MiniAmazon.MAUI.ViewModels;
using System.Xml.Serialization;

namespace MiniAmazon.MAUI.Views;

public partial class InventoryView : ContentPage
{
	public InventoryView()
	{
		InitializeComponent();
        BindingContext = new InventoryViewModel();
	}

    private void Logo_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }

    private void Add_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Product");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InventoryViewModel)?.RefreshInventory();
    }
}