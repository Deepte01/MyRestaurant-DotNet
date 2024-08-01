﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant.Core.Models
{
    public class OrderItem: BaseEntity
    {
        public string OrderId { get; set; }
        public string FoodItemId { get; set; }
        public string FoodItemName { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Quanity { get; set; }
    }
}
