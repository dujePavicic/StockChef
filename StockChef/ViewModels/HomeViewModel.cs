using SQLite;
using StockChef.Models;
using StockChef.Views;
using System.Windows.Input;

namespace StockChef.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private SQLiteAsyncConnection database;
        private string ingredientsCountText;
        private string shoppingItemsCountText;
        private string recipesCountText;

        public string IngredientsCountText
        {
            get => ingredientsCountText;
            set
            {
                ingredientsCountText = value;
                OnPropertyChanged();
            }
        }
        public string ShoppingItemsCountText
        {
            get => shoppingItemsCountText;
            set
            {
                shoppingItemsCountText = value;
                OnPropertyChanged();
            }
        }
        public string RecipesCountText
        {
            get => recipesCountText;
            set
            {
                recipesCountText = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenIngredientsCommand { get; }
        public ICommand OpenShoppingListCommand { get; }
        public ICommand OpenRecipesCommand { get; }
        public HomeViewModel()
        {
            database = App.Database;
            OpenIngredientsCommand = new Command(async () => await OpenIngredients());
            OpenShoppingListCommand = new Command(async () => await OpenShoppingList());
            OpenRecipesCommand = new Command(async () => await OpenRecipes());
            _ = LoadDashboardData();
        }

        public async Task LoadDashboardData()
        {
            int ingredientsCount = await database.Table<Ingredient>().Where(i => i.UserId == App.LoggedUser.Id).CountAsync();
            int shoppingItemsCount = await database.Table<ShoppingItem>().Where(i => i.UserId == App.LoggedUser.Id).CountAsync();

            IngredientsCountText = $"Namirnice: {ingredientsCount}";
            ShoppingItemsCountText = $"Shopping stavke: {shoppingItemsCount}";
            RecipesCountText = "Recepti: API kolekcija";
        }

        private async Task OpenIngredients()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new IngredientsPage());
        }

        private async Task OpenShoppingList()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ShoppingListPage());
        }

        private async Task OpenRecipes()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RecipesPage());
        }
    }
}