using MiniAmazon.MAUI.ViewModels;

namespace MiniAmazon.MAUI.Views;

public partial class InventoryManagementView : ContentPage
{
	public InventoryManagementView()
	{
		InitializeComponent();
		BindingContext = new InventoryManagementViewModel();
	}

	private void Logo_Clicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}

	private void Add_Clicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Product");
	}

    private void ContentPage_NavigatedTo(object sender, NavigationEventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.RefreshProducts();
    }

    private void ContentPage_NavigatedFrom(object sender, NavigationEventArgs e)
    {
        
    }
}