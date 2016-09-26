using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooApp.Models;

namespace ZooApp.ViewModels
{
    public class FoodSummayByAnimalViewModel
    {
        public FoodSummayByAnimalViewModel()
        {

        }
        public FoodSummayByAnimalViewModel(AnimalFood animalFood)
        {
            Id = animalFood.Id;
            AnimalName = animalFood.Animal.AnimalName;
            TotalQuantity = animalFood.Animal.AnimalQuantity * animalFood.FoodQuantity;
            TotalPrice = TotalQuantity * animalFood.Food.UnitPrice;
        }
        public int Id { get; set; }
        public string AnimalName { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }


    }
}
