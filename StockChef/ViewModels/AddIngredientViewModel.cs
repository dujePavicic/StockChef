using SQLite;
using StockChef.Models;
using System.Windows.Input;

namespace StockChef.ViewModels
{
    public class AddIngredientViewModel : BaseViewModel
    {
        private SQLiteAsyncConnection database;

        private string name;
        private string category;
        private string selectedLocation;
        private int quantity;
        private DateTime expirationDate = DateTime.Today;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public string Category
        {
            get => category;
            set
            {
                category = value;
                OnPropertyChanged();
            }
        }
        public string SelectedLocation
        {
            get => selectedLocation;
            set
            {
                selectedLocation = value;
                OnPropertyChanged();
            }
        }

        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnPropertyChanged();
            }
        }

        public DateTime ExpirationDate
        {
            get => expirationDate;
            set
            {
                expirationDate = value;
                OnPropertyChanged();
            }
        }
        public ICommand SaveIngredientCommand { get; }

        public AddIngredientViewModel()
        {
            database = App.Database;
            SaveIngredientCommand = new Command(async () => await SaveIngredient());
        }

        private async Task SaveIngredient()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Category) || string.IsNullOrWhiteSpace(SelectedLocation))
            {
                await Application.Current.MainPage.DisplayAlert("Greška","Popunite sva polja.","OK");
                return;
            }

            if (Quantity <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Greška","Količina mora biti veća od 0.","OK");
                return;
            }

            Ingredient ingredient = new Ingredient
            {
                UserId = App.LoggedUser.Id,
                Name = Name,
                Category = Category,
                Location = SelectedLocation,
                Quantity = Quantity,
                ExpirationDate = ExpirationDate
            };

            await database.InsertAsync(ingredient);
            await Application.Current.MainPage.DisplayAlert("Bravo","Namirnica je uspješno spremljena.","OK");
            ClearFields();
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void ClearFields()
        {
            Name = string.Empty;
            Category = string.Empty;
            SelectedLocation = null;
            Quantity = 0;
            ExpirationDate = DateTime.Today;
        }
    }
}