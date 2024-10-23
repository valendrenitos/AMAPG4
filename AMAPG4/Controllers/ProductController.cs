using AMAPG4.Models.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace AMAPG4.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDal _productDal;

        public ProductController()
        {
            _productDal = new ProductDal();
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Read(int id)
        {
            Product product = _productDal.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
