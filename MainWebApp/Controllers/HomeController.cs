using MainWebApp.DAL;
using MainWebApp.Models;
using MainWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MainWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appContext;

        public HomeController(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        public IActionResult Error(int? code)
        {
            // Handle different error codes if needed
            return View();
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Sliders = _appContext.Sliders.ToList(),
                Products = _appContext.Products.Include(x=>x.Category).ToList()
            };
            return View(homeVM);
        }

        public IActionResult Shop()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Account_Dashboard()
        {
            return View();
        }

        public IActionResult Account_Edit()
        {
            return View();
        }

        public IActionResult Account_Edit_Address()
        {
            return View();
        }

        public IActionResult Account_Orders()
        {
            return View();
        }

        public IActionResult Account_Wishlist()
        {
            return View();
        }

        public async Task <IActionResult> Blog_List()
        {
            List<Blog> blogs = await _appContext.Blogs.ToListAsync();
            return View(blogs);
        }

        public IActionResult Blog_Single(int id)
        {
            var blog = _appContext.Blogs.FirstOrDefault(x => x.Id == id);
            return View(blog);
        }

        public IActionResult Coming_Soon()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Faq()
        {
            return View();
        }

        public IActionResult Product_Variable()
        {
            return View();
        }

        public IActionResult Product_Outofstock()
        {
            return View();
        }

        public IActionResult Shop_Cart()
        {
            return View();
        }

        public IActionResult Shop_Checkout()
        {
            return View();
        }

        public IActionResult Shop_Order_Complete()
        {
            return View();
        }

        public IActionResult CardPayment()
        {
            return View();
        }

        public IActionResult Shop_Details()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}