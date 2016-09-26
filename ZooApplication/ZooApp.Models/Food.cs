using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooApp.Models
{
    public class Food : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Index("Ix_FoodName", Order = 1, IsUnique = true)]
        [Display(Name = "Food Name")]
        public string FoodName { get; set; }
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {

            ZooContext db = new ZooContext();
            var validateName = db.Foods.FirstOrDefault(x => x.FoodName == FoodName && x.Id != Id);
            if (validateName != null)
            {
                ValidationResult errorMessage = new ValidationResult("The Name is already exists.Please try a new name.", new[] { "FoodName" });
                yield return errorMessage;
            }
            else
            {
                yield return ValidationResult.Success;
            }

        }
        public virtual ICollection<AnimalFood> AnimalFoods { get; set; }

       
    }
}
