using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant.Core.ViewModels
{
    public class CartSummaryViewModel
    {
        public int CartItemsCount { get; set; }
        public decimal TotalCartValue { get; set; }

        public CartSummaryViewModel()
        {
        }

        public CartSummaryViewModel(int CartItemsCount, decimal TotalCartValue)
        {
            this.CartItemsCount = CartItemsCount;
            this.TotalCartValue = TotalCartValue;
        }
    }
}
