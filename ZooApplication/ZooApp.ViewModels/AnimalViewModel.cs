using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ZooApp.Models;

namespace ZooApp.ViewModels
{
    public class AnimalViewModel : IValidatableObject
    {
        public AnimalViewModel()
        {

        }
        public AnimalViewModel(Animal animal)
        {
            Id = animal.Id;
            AnimalName = animal.AnimalName;
            Origin = animal.Origin;
            AnimalQuantity = animal.AnimalQuantity;
        }
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Animal Name")]
        [Index("Ix_AnimalName", Order =1,IsUnique =true)]
        //[Remote("IsAnimalNameExist", "Animal", ErrorMessage = "Animal Name Already Exists")]
        public string AnimalName { get; set; }
        [Required]
        [StringLength(50)]
        public string Origin { get; set; }
        public int AnimalQuantity { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {

            ZooContext db = new ZooContext();
            var validateName = db.Animals.FirstOrDefault(x => x.AnimalName == AnimalName && x.Id != Id);
            if (validateName != null)
            {
                ValidationResult errorMessage = new ValidationResult("The Name is already exists.Please try a new name.", new[] { "AnimalName" });
                yield return errorMessage;
            }
            else
            {
               yield return ValidationResult.Success;
            }

        }
    }

}
