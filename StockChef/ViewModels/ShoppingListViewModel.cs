using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockChef.ViewModels
{
    public class ShoppingListViewModel:BaseViewModel
    {
        private SQLiteAsyncConnection database;

        public ShoppingListViewModel()
        {
            database = App.Database;
        }
    }
}
