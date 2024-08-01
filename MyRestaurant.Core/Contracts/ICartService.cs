using MyRestaurant.Core.Models;
using MyRestaurant.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyRestaurant.Core.Contracts
{
    public interface ICartService
    {
        void AddToCart(HttpContextBase httpContext, string foodItemId , string UserId);
        void RemoveFromcart(HttpContextBase httpContext, string itemId , string UserId);
        List<CartItemViewModel> GetCartItems(HttpContextBase httpContext, string UserId);
        CartSummaryViewModel GetBasketSummary(HttpContextBase httpContext, string UserId); 
        void ClearCart(HttpContextBase httpContext, string UserId);
        Cart GetCart(HttpContextBase httpContext, bool createIfNull, string UserId);
    }
}
