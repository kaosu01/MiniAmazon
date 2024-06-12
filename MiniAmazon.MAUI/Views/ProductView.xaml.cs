using MiniAmazon.MAUI.ViewModels;
namespace MiniAmazon.MAUI.Views;

[QueryProperty(nameof(ProductId),"productId")]
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
		Shell.Current.GoToAsync("//InventoryManagement");
	}

	private void Save_Clicked(object sender, EventArgs e)
	{
		(BindingContext as ProductViewModel)?.Add();
		Shell.Current.GoToAsync("//InventoryManagement");
	}

	private void ContentPage_NavigatedTo(object sender, NavigationEventArgs e)
	{
		BindingContext = new ProductViewModel(ProductId);
	}
}