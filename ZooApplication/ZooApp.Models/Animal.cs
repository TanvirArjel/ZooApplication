using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooApp.Models
{
    public class Animal : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Index("Ix_AnimalName", Order =1, IsUnique = true)]
        [Display(Name ="Animal Name")]
        public string AnimalName { get; set; }
        [Required]
        [StringLength(50)]
        [Index("Ix_AnimalOrigin")]

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

        public virtual ICollection<AnimalFood> AnimalFoods { get; set; }

    }

}
