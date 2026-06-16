using StockChef.ViewModels;

namespace StockChef.Views;

public partial class ShoppingListPage : ContentPage
{
    private ShoppingListViewModel viewModel;

    public ShoppingListPage()
    {
        InitializeComponent();

        viewModel = new ShoppingListViewModel();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await viewModel.LoadShoppingItems();
    }
}