using StockChef.ViewModels;

namespace StockChef.Views;

public partial class IngredientsPage : ContentPage
{
    private IngredientViewModel viewModel;

    public IngredientsPage()
    {
        InitializeComponent();

        viewModel = new IngredientViewModel();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await viewModel.LoadIngredients();
    }
}