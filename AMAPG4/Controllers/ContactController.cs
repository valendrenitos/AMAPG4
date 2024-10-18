using Microsoft.AspNetCore.Mvc;

namespace AMAPG4.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
