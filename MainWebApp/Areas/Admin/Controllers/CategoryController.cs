using MainWebApp.DAL;
using MainWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MainWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            return View(_appDbContext.Categories.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            _appDbContext.Categories.Add(category);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var editCategory = _appDbContext.Categories.Find(id);
            if (editCategory != null)
            {
                return View(editCategory);
            }
            return NotFound();
        }


        [HttpPost]
        public IActionResult Edit(Category category)
        {
            var oldCategory = _appDbContext.Categories.Find(category.Id);
            if (oldCategory != null)
            {
                oldCategory.Name = category.Name;
                _appDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var currentCategory = _appDbContext.Categories.Find(id);

            if (currentCategory != null)
            {
                _appDbContext.Categories.Remove(currentCategory);
                _appDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
