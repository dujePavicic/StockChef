using StockChef.ViewModels;

namespace StockChef.Views;

public partial class AddShoppingItemPage : ContentPage
{
    public AddShoppingItemPage()
    {
        InitializeComponent();
        BindingContext = new AddShoppingItemViewModel();
    }
}