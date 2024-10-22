using Microsoft.AspNetCore.Mvc;

namespace AMAPG4.Controllers
{
    public class OrganizationController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
