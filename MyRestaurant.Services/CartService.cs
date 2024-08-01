using MyRestaurant.Core.Contracts;
using MyRestaurant.Core.Models;
using MyRestaurant.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyRestaurant.Services
{
    public class CartService : ICartService
    {
        IRepository<FoodItems> foodContext;
        IRepository<Cart> cartContext;
        IRepository<CartItem> cartItemContext;
        public const string cartSessionName = "eCommercecart";
        public CartService(IRepository<FoodItems> foodContext, IRepository<Cart> cartContext , IRepository<CartItem> cartItemContext)
        {
            this.foodContext = foodContext;
            this.cartContext = cartContext;
            this.cartItemContext = cartItemContext;
        }
        public Cart updateCartUserId(Cart cart, string UserId)
        {
            Cart updateCart= cartContext.Find(cart.Id);
            updateCart.UserId = UserId;
            cartContext.Update(updateCart);
            cartContext.Commit();
            return updateCart;
        }
        public  Cart GetCart(HttpContextBase httpContext, bool createIfNull, string UserId)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(cartSessionName);

            Cart cart = new Cart();

            if (cookie != null)
            {
                string cartId = cookie.Value;
                if (!string.IsNullOrEmpty(cartId))
                {
                    cart = cartContext.Find(cartId);
                    if(cart.CartProcessed == false)
                    {
                        if (cart.UserId == null)
                        {
                            cart = updateCartUserId(cart, UserId);
                        }
                    }
                    else
                    {
                        cart = CreateNewcart(httpContext, UserId);
                    }
                }
                else
                {
                    if (createIfNull)
                    {
                        cart = CreateNewcart(httpContext, UserId);
                    }
                }
            }
            else
            {
                if (createIfNull)
                {
                    cart = CreateNewcart(httpContext, UserId);
                }
            }

            return cart;

        }

        private Cart CreateNewcart(HttpContextBase httpContext, string UserId)
        {
            Cart cart = new Cart();
            cart.UserId = UserId;
            cartContext.Insert(cart);
            cartContext.Commit();

            HttpCookie cookie = new HttpCookie(cartSessionName);
            cookie.Value = cart.Id;
            cookie.Expires = DateTime.Now.AddHours(1);
            httpContext.Response.Cookies.Add(cookie);

            return cart;
        }
        public void AddToCart(HttpContextBase httpContext, string foodItemId, string UserId)
        {
            Cart cart = GetCart(httpContext, true,  UserId);
            cart.UserId = UserId;

            CartItem item = cart.CartItems.FirstOrDefault(i => i.FoodItemId == foodItemId);

            if (item == null)
            {
                item = new CartItem()
                {
                    CartId = cart.Id,
                    FoodItemId = foodItemId,
                    Quanity = 1
                };

                cart.CartItems.Add(item);
            }
            else
            {
                item.Quanity = item.Quanity + 1;
            }

            cartContext.Commit();
        }

        public void RemoveFromcart(HttpContextBase httpContext, string itemId, string UserId)
        {
            Cart cart = GetCart(httpContext, true , UserId);
            CartItem item = cart.CartItems.FirstOrDefault(i => i.FoodItemId == itemId);

            if (item != null)
            {
                cart.CartItems.Remove(item);
                cartContext.Commit();
                cartItemContext.Delete(item.Id);
                cartItemContext.Commit();
            }
        }

        public List<CartItemViewModel> GetCartItems(HttpContextBase httpContext, string UserId)
        {
            Cart cart = GetCart(httpContext, true, UserId);
            if(cart != null)
            {
                var result = (from c in cart.CartItems
                              join f in foodContext.Collection()
                              on c.FoodItemId equals f.Id
                              select new CartItemViewModel()
                              {
                                  Id = c.FoodItemId,
                                  Quanity = c.Quanity,
                                  Image = f.Image,
                                  Price = f.Price,
                                  FoodItemName = f.Name
                              }).ToList();
                return result;
            }
            else
            {
                return new List<CartItemViewModel>();
            }
        }

        public CartSummaryViewModel GetBasketSummary(HttpContextBase httpContext, string UserId)
        {
            Cart cart = GetCart(httpContext, false, UserId);
            CartSummaryViewModel model = new CartSummaryViewModel(0, 0);
            if (cart != null)
            {
                int? basketCount = cart.CartItems.Select(x => x.Quanity).Sum();

                decimal? basketTotal = (from item in cart.CartItems
                                        join p in foodContext.Collection() on item.FoodItemId equals p.Id
                                        select item.Quanity * p.Price).Sum();
                model.CartItemsCount = basketCount ?? 0;
                model.TotalCartValue = basketTotal ?? decimal.Zero;
                return model;
            }
            else
            {
                return model;
            }
        }

        public void ClearCart(HttpContextBase httpContext, string UserId)
        {
            Cart cart = GetCart(httpContext, false, UserId);
            cart.CartItems.Clear();
            cart.CartProcessed = true;
            cartContext.Update(cart);
            cartContext.Commit();
        }

    }
}
