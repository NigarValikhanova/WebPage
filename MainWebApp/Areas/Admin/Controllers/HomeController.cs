using MainWebApp.DAL;
using Microsoft.AspNetCore.Mvc;

namespace MainWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
