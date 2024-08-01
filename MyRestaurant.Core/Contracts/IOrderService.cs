using MyRestaurant.Core.Models;
using MyRestaurant.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public interface IOrderService
    {
        void createOrder(Order baseOrder, List<CartItemViewModel> cartItems);
    }
}
