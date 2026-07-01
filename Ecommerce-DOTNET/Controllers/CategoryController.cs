using Ecommerce_DOTNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_DOTNET.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ProjectDbContext context;

        public IWebHostEnvironment Env { get; }

        public IActionResult Index()
        {
            return View();
        }

        public CategoryController(ProjectDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            Env = env;
        }

        //create 
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category, IFormFile Image)
        {
            string fileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
            string path = Path.Combine(Env.WebRootPath, "Images/", fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                Image.CopyTo(stream);
            }
            category.Image = fileName;
            this.context.Categories.Add(category);
            this.context.SaveChanges();
            return RedirectToAction("Read");
        }

        //Read
        public IActionResult Read()
        {
            var data = this.context.Categories.ToList();
            return View(data);
        }

        //Delete
        public IActionResult Delete(int Id)
        {
            var modeldata = this.context.Categories.FirstOrDefault(x => x.Id == Id);
            this.context.Categories.Remove(modeldata);
            this.context.SaveChanges();
            return RedirectToAction("Read");
        }
        //Update 
        public  IActionResult Edit(int Id)
        {
            var modeldata = this.context.Categories.FirstOrDefault(x => x.Id == Id);
            return View(modeldata);
        }

        [HttpPost]
        public IActionResult Edit(Category category, IFormFile Image)
        {
            var modeldata = this.context.Categories.FirstOrDefault(x => x.Id == category.Id);
            modeldata.CategoryName = category.CategoryName; 
             if(Image != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                string path = Path.Combine(Env.WebRootPath, "Images/", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }
                modeldata.Image = fileName;
            }
             this.context.SaveChanges();
            return RedirectToAction("Read");
        }
    }
}
