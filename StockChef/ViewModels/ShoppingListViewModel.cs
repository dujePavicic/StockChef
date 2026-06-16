using SQLite;
using StockChef.Models;
using StockChef.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StockChef.ViewModels
{
    public class ShoppingListViewModel : BaseViewModel
    {
        private SQLiteAsyncConnection database;
        public ObservableCollection<ShoppingItem> ShoppingItems { get; set; }
        public ICommand OpenAddShoppingItemCommand { get; }
        public ICommand DeleteShoppingItemCommand { get; }

        public ShoppingListViewModel()
        {
            database = App.Database;
            ShoppingItems = new ObservableCollection<ShoppingItem>();
            OpenAddShoppingItemCommand = new Command(async () => await OpenAddShoppingItem());
            DeleteShoppingItemCommand = new Command<ShoppingItem>(async (item) => await DeleteShoppingItem(item));
            _ = LoadShoppingItems();
        }
        public async Task LoadShoppingItems()
        {
            var items = await database.Table<ShoppingItem>().Where(i => i.UserId == App.LoggedUser.Id).ToListAsync();

            ShoppingItems.Clear();

            foreach (var item in items)
            {
                ShoppingItems.Add(item);
            }
        }
        private async Task DeleteShoppingItem(ShoppingItem item)
        {
            if (item == null)
                return;

            bool confirm = await Application.Current.MainPage.DisplayAlert("Izbriši namirnicu ",$"Izbrisati namirnicu {item.Name}?","Da","Ne");

            if (!confirm)
                return;

            await database.DeleteAsync(item);
            ShoppingItems.Remove(item);
        }

        private async Task OpenAddShoppingItem()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddShoppingItemPage());
        }
    }
}