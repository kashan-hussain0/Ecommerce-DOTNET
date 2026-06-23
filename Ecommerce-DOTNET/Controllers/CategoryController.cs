using Ecommerce_DOTNET.Models;
using Microsoft.AspNetCore.Mvc;

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
            return RedirectToAction("Index", "Home");
        }

        //Read
        public IActionResult Read()
        {
            var data = this.context.Categories.ToList();
            return View(data);
        }

    }
}
