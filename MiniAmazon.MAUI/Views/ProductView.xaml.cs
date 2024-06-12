using MiniAmazon.MAUI.ViewModels;
namespace MiniAmazon.MAUI.Views;

public partial class ProductView : ContentPage
{
	public int ProductId { get; set; }

	public ProductView()
	{
		InitializeComponent();
		BindingContext = new ProductViewModel();
	}

	private void Cancel_Clicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Inventory");
	}

	private void Save_Clicked(object sender, EventArgs e)
	{
		(BindingContext as ProductViewModel)?.Add();
		Shell.Current.GoToAsync("//Inventory");
	}
}