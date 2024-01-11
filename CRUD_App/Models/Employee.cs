using System.ComponentModel.DataAnnotations;

namespace CRUDApplication.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Salaray { get; set; }
        public string Department { get; set; }  
    }
}
