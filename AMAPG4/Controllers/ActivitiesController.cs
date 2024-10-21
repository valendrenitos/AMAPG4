using AMAPG4.Models.Catalog;
using AMAPG4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AMAPG4.ViewModels;

namespace AMAPG4.Controllers
{
    public class ActivitiesController : Controller
    {
        public MyDBContext _bddContext;
        private ProductDal _productDal;

        public ActivitiesController()
        {
            _productDal = new ProductDal();
        }

        public IActionResult Index()
        {
            List<Product> products = _productDal.GetAllActivityProducts();

            ActivitiesViewModel viewModel = new ActivitiesViewModel
            {
                Products = products,
            };

            return View("/Views/LaFerme/Activities/Index.cshtml", viewModel);
        }
    }
}
