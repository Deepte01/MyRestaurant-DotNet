using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MyRestaurant.Core.Contracts;
using MyRestaurant.Core.Models;
using MyRestaurant.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyRestaurant.WebUI.Controllers
{
    public class CartController : Controller
    {
        ICartService cartService;
        IOrderService orderService;
        IRepository<Customer> customers;
        private ApplicationUserManager _userManager;
        public CartController(ICartService cartService , IOrderService orderService, IRepository<Customer> customers)
        {
            this.cartService = cartService;
            this.orderService = orderService;
            this.customers = customers;
        }
        // GET: Cart
        public ActionResult Index()
        {
            var model = cartService.GetCartItems(this.HttpContext, User.Identity.GetUserId());
            return View(model);
        }
        public ActionResult AddToCart(string Id)
        {
            cartService.AddToCart(this.HttpContext, Id , User.Identity.GetUserId());
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(string Id)
        {
            cartService.RemoveFromcart(this.HttpContext, Id, User.Identity.GetUserId());

            return RedirectToAction("Index");
        }
        public PartialViewResult CartSummary()
        {
            var cartSummary = cartService.GetBasketSummary(this.HttpContext, User.Identity.GetUserId());

            return PartialView(cartSummary);
        }
        [Authorize]
        public async Task<ActionResult> Checkout()
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            //Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            if(user != null)
            {
                Order order = new Order()
                {
                    Email = user.Email,
                    City = user.City,
                    State = user.State,
                    Street = user.Street,
                    FirstName = user.FirstName,
                    Surname = user.LastName,
                    ZipCode = user.ZipCode,
                    OrderdUserId = User.Identity.GetUserId()
                };
                return View(order);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult Checkout(Order order)
        {
            var foodItems = cartService.GetCartItems(this.HttpContext, User.Identity.GetUserId());
            order.OrderStatus = "Order Created";
            order.Email = User.Identity.Name;
            //process payment
            order.OrderStatus = "Payment Processed";
            order.CartId = cartService.GetCart(this.HttpContext, false, User.Identity.GetUserId()).Id;
            orderService.createOrder(order, foodItems);
            cartService.ClearCart(this.HttpContext , User.Identity.GetUserId());

            return RedirectToAction("Thankyou", new { OrderId = order.Id });
        }
        public ActionResult Thankyou(string OrderId)
        {
            ViewBag.OrderId = OrderId;
            return View();
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

    }
}