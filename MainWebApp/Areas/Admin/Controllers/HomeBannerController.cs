using Microsoft.AspNetCore.Mvc;

namespace MainWebApp.Areas.Admin.Controllers
{
    public class HomeBannerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
