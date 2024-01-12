using System.ComponentModel.DataAnnotations;

namespace CRUDApplication.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "* Name should be filled")]
        public string Name { get; set; }
        [Required(ErrorMessage = "* Email should be filled")]
        public string Email { get; set; }
        [Required(ErrorMessage = "* Salary should be filled")]
        public decimal Salaray { get; set; }

        [Required(ErrorMessage = "* Department should be filled")]
        public string Department { get; set; }
    }
}
