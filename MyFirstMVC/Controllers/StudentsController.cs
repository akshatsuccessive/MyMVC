using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstMVCCore.Data;
using MyFirstMVCCore.Models;
using MyFirstMVCCore.Models.Domain;

namespace MyFirstMVCCore.Controllers
{
    public class StudentsController : Controller
    {
        private readonly MVCDemoDbContext context;

        public StudentsController(MVCDemoDbContext context)
        {
            this.context = context;
        }


        // showing all the students on index
        [HttpGet] 
        public async Task<IActionResult> Index()
        {
            var students = await context.Students.ToListAsync();
            return View(students);
        }


        // adding a new student
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentsViewModel req)
        {
            var student = new Student()
            {
                Id = Guid.NewGuid(),
                Name = req.Name,
                Email = req.Email,
                DOB = req.DOB,
                City = req.City
            };

            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ViewAll(Guid Id)
        {
            var stud = await context.Students.FirstOrDefaultAsync(x => x.Id == Id);
            if(stud != null)
            {
                var viewModel = new UpdateStudentViewModel()
                {
                    Id = Guid.NewGuid(),
                    Name = stud.Name,
                    Email = stud.Email,
                    DOB = stud.DOB,
                    City = stud.City
                };
                return View(viewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ViewAll(UpdateStudentViewModel req)
        {
            var student = await context.Students.FindAsync(req.Id);
            if(student != null) 
            {
                student.Name = req.Name;
                student.Email = req.Email;  
                student.DOB = req.DOB;
                student.City = req.City;
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateStudentViewModel req)
        {
            var student = await context.Students.FindAsync(req.Id);
            if (student != null)
            {
                context.Students.Remove(student);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
