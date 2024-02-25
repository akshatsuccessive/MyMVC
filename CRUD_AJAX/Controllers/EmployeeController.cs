using CRUD_AJAX.Data;
using CRUD_AJAX.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_AJAX.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext context;
        public EmployeeController(ApplicationDbContext context)
        {
            this.context = context;
        }


        public IActionResult Index()
        {
            return View(context.Employees.ToList());
        }
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if(ModelState.IsValid)
            {
                var newEmployee = new Employee()
                {
                    EmployeeId = Guid.NewGuid(),
                    Name = employee.Name,
                    Address = employee.Address,
                    Department = employee.Department,
                    salary = employee.salary     
                };

                await context.Employees.AddAsync(newEmployee);  
                await context.SaveChangesAsync();
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", context.Employees.ToList())});   
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddEmployee", employee) });
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployee(Guid id)
        {
            var employee = await context.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        public async Task<IActionResult> EditEmployee(Employee employee)
        {
            if(ModelState.IsValid)
            {
                var existingEmployee = await context.Employees.FindAsync(employee.EmployeeId);
                if(existingEmployee == null)
                {
                    return NotFound();
                }
                existingEmployee.Name = employee.Name;
                existingEmployee.Address = employee.Address;
                existingEmployee.salary = employee.salary;
                existingEmployee.Department = employee.Department;

                await context.SaveChangesAsync();
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", context.Employees.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "EditEmployee", employee) });
        }



        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await context.Employees.FindAsync(id);
            if(employee == null) 
            {
                return NotFound();
            }
            context.Employees.Remove(employee);
            await context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", context.Employees.ToList()) });
        }
    }
}
