using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant.Core.Models
{
    public class Cart : BaseEntity
    {
        public virtual ICollection<CartItem> CartItems { get; set; }
        public string UserId { get; set; }
        public bool CartProcessed { get; set; }
        public Cart()
        {
            this.CartItems = new List<CartItem>();
            this.CartProcessed = false;
        }
    }
}
