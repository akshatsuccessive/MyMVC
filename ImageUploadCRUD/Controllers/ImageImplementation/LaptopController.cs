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



        public async Task<IActionResult> Delete(int Id)
        {
            if(Id == 0)
            {
                return NotFound();
            }
            else
            {
                var laptop = await _context.Laptops.FindAsync(Id);
                if(laptop == null)
                {
                    return NotFound();
                }
                else
                {
                    string deleteFromFolder = Path.Combine(_environment.WebRootPath, "Content/Laptop");
                    string currentImage = Path.Combine(Directory.GetCurrentDirectory(), deleteFromFolder, laptop.Path);
                    if(currentImage != null)
                    {
                        if(System.IO.File.Exists(currentImage))
                        {
                            System.IO.File.Delete(currentImage);    
                        }
                    }
                    _context.Laptops.Remove(laptop);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Record Deleted Successfully";
                }
            }
            return RedirectToAction("Index");   
        }

        //  view details
        public async Task<IActionResult> Details(int id)
        {
            var laptop = await _context.Laptops.FindAsync(id);
            return View(laptop);
        }
        

        // edit
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var laptop = await _context.Laptops.FindAsync(id);
                if(laptop == null)
                {
                    return RedirectToAction("Index");
                }
                return View(laptop);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Laptop model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var laptop = await _context.Laptops.FindAsync(model.Id);
                    string uniqueFileName = string.Empty;
                    if(model.ImagePath != null)
                    {
                        if(laptop.Path != null)
                        {
                            string filePath = Path.Combine(_environment.WebRootPath, "Content/Laptop", laptop.Path);
                            if(System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }
                        }
                        uniqueFileName = UploadImage(model);
                    }
                    laptop.Brand = model.Brand;
                    laptop.Color = model.Color;
                    if(model.ImagePath != null)
                    {
                        laptop.Path = uniqueFileName;
                    }
                    _context.Laptops.Update(laptop); 
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Record Updated Successfully";
                }
                else
                {
                    return View(model);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
