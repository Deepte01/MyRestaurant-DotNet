using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyRestaurant.Core.Models;

namespace MyRestaurant.DataAccess.InMemory
{
    public class FoodItemsRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<FoodItems> foodItems;
        public FoodItemsRepository()
        {
            foodItems = cache["foodItems"] as List<FoodItems>;
            if(foodItems == null)
            {
                foodItems = new List<FoodItems>();
            }
        }
        public void Commit()
        {
            cache["foodItems"] = foodItems;
        }
        public void Insert(FoodItems foodItem)
        {
            foodItems.Add(foodItem);
        }
        public void Update(FoodItems foodItem)
        {
            FoodItems itemToUpdate = foodItems.Find(p => p.Id == foodItem.Id);
            if(itemToUpdate != null)
            {
                itemToUpdate = foodItem;
            }
            else
            {
                throw new Exception("Food Item not found");
            }
        }
        public FoodItems Find(string id)
        {
            FoodItems item = foodItems.Find(p => p.Id == id);
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new Exception("Food Item not found");
            }
        }
        public IQueryable<FoodItems> collection()
        {
            return foodItems.AsQueryable();
        }
        public void Delete(string Id)
        {
            FoodItems itemToDelete = foodItems.Find(p => p.Id == Id);
            if (itemToDelete != null)
            {
                foodItems.Remove(itemToDelete);
            }
            else
            {
                throw new Exception("Food Item not found");
            }
        }
    }
}
