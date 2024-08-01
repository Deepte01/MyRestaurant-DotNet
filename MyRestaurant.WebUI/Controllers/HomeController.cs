using MyRestaurant.Core.Contracts;
using MyRestaurant.Core.Models;
using MyRestaurant.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyRestaurant.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<FoodItems> context;
        IRepository<FoodCategory> foodCategories;
        public HomeController(IRepository<FoodItems> foodItemsContext, IRepository<FoodCategory> foodCategoryContext)
        {
            context = foodItemsContext;
            foodCategories = foodCategoryContext;
        }
        public ActionResult Index(string Category = null)
        {
            List<FoodItems> foodItems;
            List<FoodCategory> categories = foodCategories.Collection().ToList();
            if(Category == null)
            {
                foodItems = context.Collection().ToList();
            }
            else
            {
                foodItems = context.Collection().Where(p => p.Category.Category == Category).ToList();
            }
            FoodListViewModel model = new FoodListViewModel();
            model.FoodItems = foodItems;
            model.FoodCategories = categories;
            return View(model);
        }

        public ActionResult Details(string Id)
        {
            FoodItems item = context.Find(Id);
            if(item==null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(item);
            }
           
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}