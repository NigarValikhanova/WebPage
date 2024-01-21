using MainWebApp.DAL;
using MainWebApp.Extensions;
using MainWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MainWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public BlogController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            return View(_appDbContext.Blogs.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Create(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return View(blog);
            }
            if (FileExtension.IsImage(blog.File))
            {
                string ad = await FileExtension.SaveAsync(blog.File, "blog");
                blog.ImageUrl = ad;
                _appDbContext.Blogs.Add(blog);
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
            var editBlog = _appDbContext.Blogs.Find(id);
            if (editBlog != null)
            {
                return View(editBlog);
            }
            return NotFound();
        }


        [HttpPost]
        public async Task <IActionResult> Edit(Blog blog)
        {
            var oldBlog = _appDbContext.Blogs.Find(blog.Id);
            if (oldBlog != null)
            {
                if (FileExtension.IsImage(blog.File))
                {
                    string ad = await FileExtension.SaveAsync(blog.File, "blog");
                    oldBlog.ImageUrl = ad;
                    oldBlog.Name = blog.Name;
                    oldBlog.UserName = blog.UserName;
                    oldBlog.Context = blog.Context;
                    oldBlog.ReleaseDate = blog.ReleaseDate;

                    _appDbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var currentBlog = _appDbContext.Blogs.Find(id);

            if (currentBlog != null)
            {
                _appDbContext.Blogs.Remove(currentBlog);
                _appDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
