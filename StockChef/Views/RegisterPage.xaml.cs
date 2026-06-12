using StockChef.ViewModels;
namespace StockChef.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new RegisterViewModel();
    }
}