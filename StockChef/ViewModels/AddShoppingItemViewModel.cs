using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockChef.ViewModels
{
    public class AddShoppingItemViewModel:BaseViewModel
    {
        private SQLiteAsyncConnection database;

        public AddShoppingItemViewModel()
        {
            database = App.Database;
        }
    }
}
