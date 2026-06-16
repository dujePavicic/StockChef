using StockChef.ViewModels;

namespace StockChef.Views;

public partial class HomePage : ContentPage
{
    private HomeViewModel viewModel;

    public HomePage()
    {
        InitializeComponent();

        viewModel = new HomeViewModel();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.LoadDashboardData();
    }
}