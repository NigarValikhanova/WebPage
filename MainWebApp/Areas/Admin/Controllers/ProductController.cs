using MainWebApp.DAL;
using MainWebApp.Extensions;
using MainWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace MainWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            var products = _appDbContext.Products.Include(x => x.Category).ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Category = _appDbContext.Categories.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            ViewBag.Category = _appDbContext.Categories.ToList();
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            if (FileExtension.IsImage(product.File))
            {
                string ad = await FileExtension.SaveAsync(product.File, "products");
                product.ImageUrl = ad;
                _appDbContext.Products.Add(product);
                _appDbContext.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("Error", "Shekil formati duzgun deyil");
                return View();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var editProduct = _appDbContext.Products.Find(id);
            ViewBag.Category = _appDbContext.Categories.ToList();
            if (editProduct != null)
            {
                return View(editProduct);
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            var oldProduct = _appDbContext.Products.Find(product.Id);
            if (oldProduct != null)
            {
                if (FileExtension.IsImage(product.File))
                {
                    string ad = await FileExtension.SaveAsync(product.File, "products");
                    oldProduct.File = product.File;
                    oldProduct.ImageUrl = ad;
                    oldProduct.Name = product.Name;
                    oldProduct.Price = product.Price;
                    oldProduct.CategoryId = product.CategoryId;
                    _appDbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var currentProduct = _appDbContext.Products.Find(id);

            if (currentProduct != null)
            {
                _appDbContext.Products.Remove(currentProduct);
                _appDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
