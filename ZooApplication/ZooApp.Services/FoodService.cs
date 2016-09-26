using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooApp.Models;
using ZooApp.ViewModels;

namespace ZooApp.Services
{

    public class FoodService
    {
        ZooContext db = new ZooContext();

        public List<FoodViewModel> GetFoods()
        {
            IQueryable<Food> foods = db.Foods.OrderBy(x =>x.FoodName);
            IQueryable<FoodViewModel> viewFoods = foods.Select(food => new FoodViewModel()
            {
                Id = food.Id,
                FoodName = food.FoodName,
                UnitPrice = food.UnitPrice
            });


            //List<FoodViewModel> viewFoods = new List<FoodViewModel>();
            //foreach (var food in foods)
            //{
            //    FoodViewModel viewFood = new FoodViewModel(food);
            //    viewFoods.Add(viewFood);
            //}

            return viewFoods.ToList();
        }

        public FoodViewModel GetFoodById( int id)
        {
            Food food = db.Foods.Find(id);
            return new FoodViewModel(food);           
        }

        public void Create(FoodViewModel food)
        {
            Food foodToBeCreated = new Food()
            {
                Id = food.Id,
                FoodName = food.FoodName,
                UnitPrice = food.UnitPrice
            };
            db.Foods.Add(foodToBeCreated);
            db.SaveChanges();
        }

        public void Update(FoodViewModel food)
        {
            Food foodToBeUpdated = new Food()
            {
                Id = food.Id,
                FoodName = food.FoodName,
                UnitPrice = food.UnitPrice
            };
            db.Entry(foodToBeUpdated).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteConfirm(Food food)
        {
            Food foodToBeDeleted = db.Foods.Find(food.Id);
            db.Foods.Remove(foodToBeDeleted);
            db.SaveChanges();
        }
    }
}
