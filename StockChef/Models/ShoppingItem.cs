using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace StockChef.Models
{
    public class ShoppingItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public bool IsBought { get; set; }
    }
}
