using StockChef.ViewModels;

namespace StockChef.Views;

public partial class AddIngredientPage : ContentPage
{
    public AddIngredientPage()
    {
        InitializeComponent();
        BindingContext = new AddIngredientViewModel();
    }
}