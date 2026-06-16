using SQLite;
using StockChef.Models;
using System.Windows.Input;

namespace StockChef.ViewModels
{
    public class AddShoppingItemViewModel : BaseViewModel
    {
        private SQLiteAsyncConnection database;
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveShoppingItemCommand { get; }
        public AddShoppingItemViewModel()
        {
            database = App.Database;
            SaveShoppingItemCommand = new Command(async () => await SaveShoppingItem());
        }

        private async Task SaveShoppingItem()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Application.Current.MainPage.DisplayAlert("Greška","Unesite naziv stavke.","OK");
                return;
            }

            ShoppingItem item = new ShoppingItem
            {
                UserId = App.LoggedUser.Id,
                Name = Name,
                IsBought = false
            };

            await database.InsertAsync(item);
            await Application.Current.MainPage.DisplayAlert("Bravo","Namirnica je uspješno spremljena.","OK");
            Name = string.Empty;
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}