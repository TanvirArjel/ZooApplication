using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ZooApp.Models;


namespace ZooApp.ViewModels
{
    public class FoodViewModel : IValidatableObject
    {
        public FoodViewModel()
        {

        }
        public FoodViewModel(Food food)
        {
            Id = food.Id;
            FoodName = food.FoodName;
            UnitPrice = food.UnitPrice;
        }
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Index("Ix_FoodName", Order = 1, IsUnique = true)]
        [Remote("IsFoodNameExist", "Food", ErrorMessage = "Food Name Already Exists")]
        [Display(Name = "Food Name")]
        public string FoodName { get; set; }
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ZooContext db = new ZooContext();
            var validateFoodName = db.Foods.FirstOrDefault(x => x.FoodName == FoodName && x.Id != Id);
            if (validateFoodName != null)
            {
                ValidationResult errorMessage = new ValidationResult("The Name is already exists.Please try a new name.", new [] { "FoodName" });
                yield return errorMessage;
            }
            else
            {
                yield return ValidationResult.Success;
            }
        }
    }

}
