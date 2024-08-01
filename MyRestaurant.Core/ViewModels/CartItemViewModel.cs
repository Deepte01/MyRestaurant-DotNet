using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant.Core.ViewModels
{
    public class CartItemViewModel
    {
        public string Id { get; set; } // this is food item id
        public int Quanity { get; set; }
        public string FoodItemName { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
