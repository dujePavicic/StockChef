using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace StockChef.Models
{
    public class Recipe
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public string ImageUrl { get; set; }
    }
}
