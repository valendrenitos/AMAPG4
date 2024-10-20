using AMAPG4.Models.ContactForm;
using AMAPG4.Models.User;
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
