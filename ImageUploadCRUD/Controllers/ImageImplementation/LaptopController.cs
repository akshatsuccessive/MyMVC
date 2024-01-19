using LaptopCRUD.Data;
using LaptopCRUD.Models.ImageImplementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaptopCRUD.Controllers.ImageImplementation
{
    public class LaptopController : Controller
    {
        private readonly LaptopDBContext _context;
        private readonly IWebHostEnvironment _environment;
        public LaptopController(LaptopDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var laptops = await _context.Laptops.ToListAsync();
            return View(laptops);
        }

        // Add laptop
        public IActionResult AddLaptop()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> AddLaptop(Laptop model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string uniqueFileName = UploadImage(model);
                    var data = new Laptop()
                    {
                        Id = model.Id,
                        Brand = model.Brand,
                        Color = model.Color,
                        Path = uniqueFileName   
                    };
                    await _context.Laptops.AddAsync(data);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Record successfully saved";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Model property is not valid");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);
        }
        private string UploadImage(Laptop model)
        {
            string uniqueFileName = string.Empty;
            if(model.ImagePath != null)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "Content/Laptop");    // in wwwroot/
                uniqueFileName = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + "_" + model.ImagePath.FileName;
                string FilePath = Path.Combine(uploadFolder, uniqueFileName);
                using(var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    model.ImagePath.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
