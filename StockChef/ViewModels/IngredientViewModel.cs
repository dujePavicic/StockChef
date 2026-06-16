using SQLite;
using StockChef.Models;
using StockChef.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StockChef.ViewModels
{
    public class IngredientViewModel : BaseViewModel
    {
        private SQLiteAsyncConnection database;
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public ICommand OpenAddIngredientCommand { get; }
        public ICommand DeleteIngredientCommand { get; }
        public ICommand ShowAllCommand { get; }
        public ICommand FilterFridgeCommand { get; }
        public ICommand FilterPantryCommand { get; }
        public IngredientViewModel()
        {
            database = App.Database;
            Ingredients = new ObservableCollection<Ingredient>();

            OpenAddIngredientCommand = new Command(async () => await OpenAddIngredient());
            DeleteIngredientCommand = new Command<Ingredient>(async (ingredient) => await DeleteIngredient(ingredient));
            ShowAllCommand = new Command(async () => await LoadIngredients());
            FilterFridgeCommand = new Command(async () => await FilterByLocation("Frižider"));
            FilterPantryCommand = new Command(async () => await FilterByLocation("Ostava"));
            _ = LoadIngredients();
        }

        public async Task LoadIngredients()
        {
            var ingredients = await database.Table<Ingredient>().Where(i => i.UserId == App.LoggedUser.Id).ToListAsync();
            Ingredients.Clear();
            foreach (var ingredient in ingredients)
            {
                Ingredients.Add(ingredient);
            }
        }

        private async Task FilterByLocation(string location)
        {
            var ingredients = await database.Table<Ingredient>().Where(i => i.UserId == App.LoggedUser.Id && i.Location == location).ToListAsync();
            Ingredients.Clear();
            foreach (var ingredient in ingredients)
            {
                Ingredients.Add(ingredient);
            }
        }

        private async Task DeleteIngredient(Ingredient ingredient)
        {
            if (ingredient == null)
                return;

            bool confirm = await Application.Current.MainPage.DisplayAlert("Izbriši namirnicu",$"Izbrisati {ingredient.Name}?","Da","Ne");

            if (!confirm)
                return;

            await database.DeleteAsync(ingredient);

            Ingredients.Remove(ingredient);
        }

        private async Task OpenAddIngredient()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddIngredientPage());
        }
    }
}