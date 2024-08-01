using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant.Core.Models
{
    public class FoodCategory: BaseEntity
    {
        public string Category { get; set; }
        public FoodCategory()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
