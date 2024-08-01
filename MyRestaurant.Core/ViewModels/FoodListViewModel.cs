using MyRestaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant.Core.ViewModels
{
   public class FoodListViewModel
    {
      public IEnumerable<FoodItems> FoodItems { get; set; }
      public IEnumerable<FoodCategory> FoodCategories { get; set; }
    }
}
