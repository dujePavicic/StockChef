using StockChef.Models;
using StockChef.Views;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Input;

namespace StockChef.ViewModels
{
    public class RecipesViewModel : BaseViewModel
    {
        private HttpClient httpClient;
        private Recipe selectedRecipe;
        public ObservableCollection<Recipe> Recipes { get; set; }
        public Recipe SelectedRecipe
        {
            get => selectedRecipe;
            set
            {
                selectedRecipe = value;
                OnPropertyChanged();
                if (selectedRecipe != null)
                {
                    OpenRecipeDetails(selectedRecipe);
                }
            }
        }

        public RecipesViewModel()
        {
            httpClient = new HttpClient();
            Recipes = new ObservableCollection<Recipe>();
            _ = LoadRecipes();
        }

        private async Task LoadRecipes()
        {
            try
            {
                string url = "https://www.themealdb.com/api/json/v1/1/search.php?s=chicken";
                string response = await httpClient.GetStringAsync(url);

                MealDbResponse mealDbResponse = JsonSerializer.Deserialize<MealDbResponse>(response);

                Recipes.Clear();

                if (mealDbResponse?.Meals != null)
                {
                    foreach (var meal in mealDbResponse.Meals)
                    {
                        Recipes.Add(new Recipe
                        {
                            Id = meal.IdMeal,
                            Name = meal.StrMeal,
                            Category = meal.StrCategory,
                            Instructions = meal.StrInstructions,
                            ImageUrl = meal.StrMealThumb
                        });
                    }
                }
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Greška","Recepti se trenutno ne mogu učitati.","OK");
            }
        }

        private async void OpenRecipeDetails(Recipe recipe)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RecipeDetailsPage(recipe));
            SelectedRecipe = null;
        }
    }

    public class MealDbResponse
    {
        [JsonPropertyName("meals")]
        public List<MealDbMeal> Meals { get; set; }
    }

    public class MealDbMeal
    {
        [JsonPropertyName("idMeal")]
        public string IdMeal { get; set; }
        [JsonPropertyName("strMeal")]
        public string StrMeal { get; set; }
        [JsonPropertyName("strCategory")]
        public string StrCategory { get; set; }
        [JsonPropertyName("strInstructions")]
        public string StrInstructions { get; set; }
        [JsonPropertyName("strMealThumb")]
        public string StrMealThumb { get; set; }
    }
}