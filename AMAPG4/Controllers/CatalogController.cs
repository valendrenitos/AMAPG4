using Microsoft.AspNetCore.Mvc;

namespace AMAPG4.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
