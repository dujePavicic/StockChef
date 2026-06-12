using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockChef.ViewModels
{
    
    public class AddIngredientViewModel:BaseViewModel
    {
        private SQLiteAsyncConnection database;

        public AddIngredientViewModel()
        {
            database = App.Database;
        }
    }
}
