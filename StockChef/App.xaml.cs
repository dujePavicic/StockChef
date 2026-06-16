using SQLite;
using StockChef.Models;
using StockChef.Views;

namespace StockChef
{
    public partial class App : Application
    {
        public static SQLiteAsyncConnection Database { get; private set; }
        public static User LoggedUser { get; set; }
        public App()
        {
            InitializeComponent();

            string dbPath = Path.Combine(
                FileSystem.AppDataDirectory,
                "stockchef.db");

            Database = new SQLiteAsyncConnection(dbPath);

            Database.CreateTableAsync<User>().Wait();
            Database.CreateTableAsync<Ingredient>().Wait();
            Database.CreateTableAsync<ShoppingItem>().Wait();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new LoginPage()));
        }
    }
}