using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooApp.Models
{ 
        public class AnimalFood
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [Display(Name = "Animal Name")]
            [Required]
            public int AnimalId { get; set; }

            [Display(Name = "Food Name")]
            [Required]
            public int FoodId { get; set; }
            [Display(Name = "Food Quantity")]
            public decimal FoodQuantity { get; set; }

            public virtual Animal Animal { get; set; }
            public virtual Food Food { get; set; }
        }
}
