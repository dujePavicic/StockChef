using StockChef.Models;

namespace StockChef.ViewModels
{
    public class RecipeDetailsViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public string Instructions { get; set; }

        public string ImageUrl { get; set; }

        public RecipeDetailsViewModel(Recipe recipe)
        {
            Name = recipe.Name;
            Category = recipe.Category;
            Instructions = recipe.Instructions;
            ImageUrl = recipe.ImageUrl;
        }
    }
}