using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyRestaurant.Core.Contracts;
using MyRestaurant.Core.Models;
using MyRestaurant.Core.ViewModels;
using MyRestaurant.DataAccess.InMemory;

namespace MyRestaurant.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FoodManagerController : Controller
    {
        IRepository<FoodItems> context;
        IRepository<FoodCategory> foodCategories;
        public FoodManagerController(IRepository<FoodItems> foodItemsContext, IRepository<FoodCategory> foodCategoryContext)
        {
            context = foodItemsContext;
            foodCategories = foodCategoryContext;
        }
        // GET: FoodManager
        public ActionResult Index()
        {
            List<FoodItems> fooditems = context.Collection().ToList();
            return View(fooditems);
        }
        public ActionResult Create()
        {
            FoodManagerViewModel viewModel = new FoodManagerViewModel();

            viewModel.FoodItem = new FoodItems();
            viewModel.FoodCategories = foodCategories.Collection();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(FoodManagerViewModel fooditemVM)
        {
            if(!ModelState.IsValid)
            {
                return View(fooditemVM);
            }
            else
            {
                context.Insert(fooditemVM.FoodItem);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            FoodItems item = context.Find(Id);
            if(item!= null)
            {
                FoodManagerViewModel viewModel = new FoodManagerViewModel();
                viewModel.FoodItem = item;
                viewModel.FoodCategories = foodCategories.Collection();
                return View(viewModel);
            }
            else 
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult Edit(FoodManagerViewModel vm,string Id)
        {
            FoodItems fooditem = context.Find(Id);
            if (fooditem != null)
            {
                if(!ModelState.IsValid)
                {
                    return View(vm.FoodItem);
                }
                fooditem.Category = vm.FoodItem.Category;
                fooditem.Description = vm.FoodItem.Description;
                fooditem.Name = vm.FoodItem.Name;
                fooditem.Price = vm.FoodItem.Price;
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
            FoodItems item = context.Find(Id);
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
            FoodItems fooditem = context.Find(Id);
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