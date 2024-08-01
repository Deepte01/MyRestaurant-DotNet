using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyRestaurant.Core.Contracts;
using MyRestaurant.Core.Models;
using MyRestaurant.DataAccess.InMemory;


namespace MyRestaurant.WebUI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class FoodCategoryManagerController : Controller
    {
        // GET: FoodCategoryManager
        IRepository<FoodCategory> context;
        public FoodCategoryManagerController(IRepository<FoodCategory> categoryContext)
        {
            context = categoryContext;
        }
        // GET: FoodManager
        public ActionResult Index()
        {
            List<FoodCategory> foodcategories = context.Collection().ToList();
            return View(foodcategories);
        }
        public ActionResult Create()
        {
            FoodCategory item = new FoodCategory();
            return View(item);
        }
        [HttpPost]
        public ActionResult Create(FoodCategory item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }
            else
            {
                context.Insert(item);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            FoodCategory item = context.Find(Id);
            if (item != null)
            {
                return View(item);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult Edit(FoodCategory item, string Id)
        {
            FoodCategory categoryItem = context.Find(item.Id);
            if (categoryItem != null)
            {
                if (!ModelState.IsValid)
                {
                    return View(item);
                }
                categoryItem.Category = item.Category;
                context.Commit();

                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult Delete(string Id)
        {
            FoodCategory item = context.Find(Id);
            if (item != null)
            {
                return View(item);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            FoodCategory fooditem = context.Find(Id);
            if (fooditem != null)
            {
                if (!ModelState.IsValid)
                {
                    return View(fooditem);
                }
                context.Delete(Id);
                context.Commit();

                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}