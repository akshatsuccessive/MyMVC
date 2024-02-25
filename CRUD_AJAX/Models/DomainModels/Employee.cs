using System.ComponentModel.DataAnnotations;

namespace CRUD_AJAX.Models.DomainModels
{
    public class Employee
    {
        [Key]
        public Guid EmployeeId { get; set; }
        public string Name { get; set; }    
        public string Department { get; set; }
        public int salary { get; set; }
        public string Address { get; set; }
    }
}
