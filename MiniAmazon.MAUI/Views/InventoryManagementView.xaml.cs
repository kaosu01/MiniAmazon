using MiniAmazon.MAUI.ViewModels;

namespace MiniAmazon.MAUI.Views;

public partial class InventoryManagementView : ContentPage
{
	public InventoryManagementView()
	{
		InitializeComponent();
		BindingContext = new InventoryManagementViewModel();
	}

	private void Clicked_Back(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}
}