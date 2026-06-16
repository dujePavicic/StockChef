using StockChef.Models;
using StockChef.ViewModels;

namespace StockChef.Views;

public partial class RecipeDetailsPage : ContentPage
{
    public RecipeDetailsPage(Recipe recipe)
    {
        InitializeComponent();
        BindingContext = new RecipeDetailsViewModel(recipe);
    }
}