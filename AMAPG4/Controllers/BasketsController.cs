using AMAPG4.Models.Catalog;
using AMAPG4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AMAPG4.ViewModels;

namespace AMAPG4.Controllers
{
    public class BasketsController : Controller
    {
        public MyDBContext _bddContext;
        private ProductDal _productDal;

        public  BasketsController()
        {
            _productDal = new ProductDal();
        }

        public IActionResult Index()
        {
            List<Product> products = _productDal.GetAllBasketProducts();

            BasketsViewModel viewModel = new BasketsViewModel
            {
                Products = products,
            };

            return View("/Views/LaFerme/Baskets/Index.cshtml", viewModel);
        }
    }
}
