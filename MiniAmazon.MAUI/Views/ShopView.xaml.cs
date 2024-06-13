namespace MiniAmazon.MAUI.Views;

public partial class ShopView : ContentPage
{
	public ShopView()
	{
		InitializeComponent();
	}

    private void Logo_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }
}