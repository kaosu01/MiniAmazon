using MiniAmazon.MAUI.ViewModels;
using System.IO;

namespace MiniAmazon.MAUI.Views;

public partial class ImportView : ContentPage
{
	public ImportView()
	{
		InitializeComponent();
        BindingContext = new ImportViewModel();
	}

    private void Logo_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Inventory");
    }

    private async void FileBrowser_Clicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Select File to Import",
            FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>> { { DevicePlatform.WinUI, new[] { ".csv" } } })
        });

        if (result == null)
            return;

        var stream = await result.OpenReadAsync();

        (BindingContext as ImportViewModel).ImportFile(stream);
    }

    private void Cancel_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Inventory");
    }

    private void Import_Clicked(object sender, EventArgs e)
    {
        (BindingContext as ImportViewModel).ImportFile();
    }
}