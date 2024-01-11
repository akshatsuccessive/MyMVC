using CRUDApplication.Data;
using CRUDApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MyDBContext context;
        // now we can access the database using 'context'

        public EmployeeController(MyDBContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            // we have to display the fetched data(records) on the index
            var result = context.Employees.ToList();
            return View(result);
        }

        [HttpGet]   // By default is get,  Get Method is use for returning the view
        public IActionResult Create()
        {
            return View();
        }

        // data is added to database from application
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if(ModelState.IsValid)
            {
                // submission of form
                var emp = new Employee();
                emp.Name = employee.Name;
                emp.Salaray = employee.Salaray;
                emp.Department = employee.Department;
                emp.Email = employee.Email;

                context.Employees.Add(emp);
                context.SaveChanges();

                return RedirectToAction("Index");    // the route which are given here is after Employee/
            }
            else
            {
                TempData["error"] = "All the Fields should be filled";
                return View(employee);
            }
        }

        
        // Action for delete
        public IActionResult Delete(int id)
        {
            var emp = context.Employees.SingleOrDefault(e => e.Id == id);
            if (emp != null)
            {
                context.Employees.Remove(emp);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        // Edit Action
        public IActionResult Edit(int id)     // Get method is used for returning the view
        {
            var emp = context.Employees.Single(e => e.Id == id);
            var result = new Employee();
            result.Name = emp.Name;
            result.Email = emp.Email;
            result.Salaray = emp.Salaray;
            result.Department = emp.Department;

            return View(result);    // all the result are shown on text box
        }

        [HttpPost]
        public IActionResult Edit(Employee ExistingEmp)
        {
            var emp = new Employee();
            emp.Id = ExistingEmp.Id;
            emp.Name = ExistingEmp.Name;
            emp.Email = ExistingEmp.Email;
            emp.Salaray = ExistingEmp.Salaray;
            emp.Department = ExistingEmp.Department;

            context.Employees.Update(emp);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
