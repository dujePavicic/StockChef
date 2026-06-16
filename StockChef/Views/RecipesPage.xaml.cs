using StockChef.ViewModels;

namespace StockChef.Views;

public partial class RecipesPage : ContentPage
{
    public RecipesPage()
    {
        InitializeComponent();
        BindingContext = new RecipesViewModel();
    }
}