using MyRestaurant.Core.Contracts;
using MyRestaurant.Core.Models;
using MyRestaurant.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public class OrderService : IOrderService
    {
        IRepository<Order> orderContext;
        public OrderService(IRepository<Order> orderContext)
        {
            this.orderContext = orderContext;
        }

        public void createOrder(Order baseOrder, List<CartItemViewModel> cartItems)
        {
            foreach (var item in cartItems)
            {
                baseOrder.OrderItems.Add(new OrderItem()
                {
                    FoodItemId = item.Id,
                    Image = item.Image,
                    Price = item.Price,
                    FoodItemName = item.FoodItemName,
                    Quanity = item.Quanity
                });
            }
            orderContext.Insert(baseOrder);
            orderContext.Commit();
        }
    }
}
