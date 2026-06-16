using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace StockChef.Models
{
    public class Ingredient
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        
        
    }
}
