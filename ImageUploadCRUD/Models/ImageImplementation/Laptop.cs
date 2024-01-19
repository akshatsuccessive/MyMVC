using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaptopCRUD.Models.ImageImplementation
{
    public class Laptop
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Color { get; set; }
        [ValidateNever]
        public string Path { get; set; }

        // at the time of migaration we want that this property is not going to map (Entity Framework ignores this property)
        [NotMapped]                              
        [Display(Name = "Choose Image From your device")]
        public IFormFile ImagePath { get; set; }    
    }
}
