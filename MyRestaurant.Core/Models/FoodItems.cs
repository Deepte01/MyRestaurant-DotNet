﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant.Core.Models
{
    public class FoodItems : BaseEntity
    {
        [StringLength(20)]
        [DisplayName("Food Item Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(1,100)]
        public decimal Price { get; set; }
        [Required]
        public string CategoryId { get; set; }

        public virtual FoodCategory Category { get; set; }
        public string Image { get; set; }
        public FoodItems()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
