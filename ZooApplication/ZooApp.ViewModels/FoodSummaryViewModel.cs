using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooApp.Models;

namespace ZooApp.ViewModels
{
     public class FoodSummaryViewModel
    {
        public FoodSummaryViewModel()
        {

        }
        public FoodSummaryViewModel(AnimalFood animalFood)
        {
            FoodId = animalFood.FoodId;
            FoodName = animalFood.Food.FoodName;
            FoodPrice = animalFood.Food.UnitPrice;
            TotalFoodQuantity = animalFood.Animal.AnimalQuantity * animalFood.FoodQuantity;
            TotalPrice = TotalFoodQuantity * FoodPrice;         
        }
        public int FoodId { get; set; } 
        public string FoodName { get; set; }
        public decimal FoodPrice { get; set; }
        public decimal TotalFoodQuantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
