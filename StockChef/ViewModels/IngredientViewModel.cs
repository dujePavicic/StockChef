using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockChef.ViewModels
{
    
    public class IngredientViewModel:BaseViewModel
    {

        private SQLiteAsyncConnection database;

        public IngredientViewModel()
        {
            database = App.Database;
        }
    }
}
