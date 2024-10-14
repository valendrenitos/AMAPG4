using Microsoft.AspNetCore.Mvc;

namespace AMAPG4.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
