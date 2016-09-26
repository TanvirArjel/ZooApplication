using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooApp.Models;
using ZooApp.ViewModels;
using System.Data.Entity;
using System.Web.Mvc;
using System.Net;

namespace ZooApp.Services
{
    public class AnimalFoodService
    {

        ZooContext db = new ZooContext();


        public IEnumerable<AnimalFood> GetAllAnimalFood()
        {
            IQueryable<AnimalFood> animalFoods = db.AnimalFoods.OrderBy(x => x.Animal.AnimalName);
            return animalFoods.ToList();
        }

        public AnimalFood GetAnimalFoodById(int id)
        {
            
            AnimalFood animalFood = db.AnimalFoods.Find(id);     
            return animalFood;
        }

        public void Save(AnimalFood animalFood)
        {
            db.AnimalFoods.Add(animalFood);
            db.SaveChanges();
        }

        public void Update(AnimalFood animalFood)
        {
            db.Entry(animalFood).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(AnimalFood animalFood)
        {
            AnimalFood animalFoodToBeDeleted = db.AnimalFoods.Find(animalFood.Id);
            db.AnimalFoods.Remove(animalFoodToBeDeleted);
            db.SaveChanges();
        }



        public IEnumerable<FoodSummaryViewModel> GetFoodSummary()
        {
            IQueryable<IGrouping<int, AnimalFood>> animalFoods = db.AnimalFoods.GroupBy(x => x.FoodId);

            IQueryable<FoodSummaryViewModel> foodSummaryViewModel = from foodGroup in animalFoods
                                                                    let food = foodGroup.FirstOrDefault()
                                                                    let totalFoodQuantity = foodGroup.Sum(x => x.Animal.AnimalQuantity * x.FoodQuantity)
                                                                    select new FoodSummaryViewModel()
                                                                    {                                                                     
                                                                        FoodId =food.FoodId,
                                                                        FoodName = food.Food.FoodName,
                                                                        TotalFoodQuantity = totalFoodQuantity,
                                                                        FoodPrice = food.Food.UnitPrice,
                                                                        TotalPrice = totalFoodQuantity * food.Food.UnitPrice
                                                                        
                                                                    };
            return foodSummaryViewModel.ToList().OrderBy(x => x.FoodName);
        }

        

        public IEnumerable<FoodSummayByAnimalViewModel> GetFoodSummaryByAnimal(int ? foodId)
        {
            IQueryable<AnimalFood> animalFoods = db.AnimalFoods.Where(x => x.FoodId == foodId);
            IQueryable<FoodSummayByAnimalViewModel> foodsSummaryByAnimal = animalFoods.Select(animalFood => new FoodSummayByAnimalViewModel()
            {
                Id = animalFood.Id,
                AnimalName = animalFood.Animal.AnimalName,
                TotalQuantity = animalFood.Animal.AnimalQuantity * animalFood.FoodQuantity,
                TotalPrice = animalFood.Animal.AnimalQuantity * animalFood.FoodQuantity * animalFood.Food.UnitPrice

            });

            return foodsSummaryByAnimal.ToList().OrderBy(x => x.AnimalName);

        }
    }
}
