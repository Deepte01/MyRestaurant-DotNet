using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyRestaurant.Core.Models;

namespace MyRestaurant.DataAccess.InMemory
{
    public class FoodCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<FoodCategory> foodCategories;
        public FoodCategoryRepository()
        {
            foodCategories = cache["foodCategories"] as List<FoodCategory>;
            if (foodCategories == null)
            {
                foodCategories = new List<FoodCategory>();
            }
        }
        public void Commit()
        {
            cache["foodCategories"] = foodCategories;
        }
        public void Insert(FoodCategory foodCategory)
        {
            foodCategories.Add(foodCategory);
        }
        public void Update(FoodCategory foodCategory)
        {
            FoodCategory itemToUpdate = foodCategories.Find(p => p.Id == foodCategory.Id);
            if (itemToUpdate != null)
            {
                itemToUpdate = foodCategory;
            }
            else
            {
                throw new Exception("Food Category not found");
            }
        }
        public FoodCategory Find(string id)
        {
            FoodCategory item = foodCategories.Find(p => p.Id == id);
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new Exception("Food Category not found");
            }
        }
        public IQueryable<FoodCategory> collection()
        {
            return foodCategories.AsQueryable();
        }
        public void Delete(string Id)
        {
            FoodCategory itemToDelete = foodCategories.Find(p => p.Id == Id);
            if (itemToDelete != null)
            {
                foodCategories.Remove(itemToDelete);
            }
            else
            {
                throw new Exception("Food Category not found");
            }
        }
    }
}
