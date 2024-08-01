using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant.Core.Models
{
    public class CartItem : BaseEntity
    {
        public virtual Cart Cart { get; set; }
        public string CartId { get; set; }
        public string FoodItemId { get; set; }
        public int Quanity { get; set; }
    }
}
