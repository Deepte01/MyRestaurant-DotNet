using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyRestaurant.Core.Models;

namespace MyRestaurant.Core.ViewModels
{
    public class FoodManagerViewModel
    {
        public FoodItems FoodItem { get; set; }
        public IEnumerable<FoodCategory> FoodCategories { get; set; }
    }
}
