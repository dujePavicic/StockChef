using StockChef.ViewModels;

namespace StockChef.Views;


public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}