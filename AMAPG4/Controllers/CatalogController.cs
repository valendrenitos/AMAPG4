using AMAPG4.Models.Catalog;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMAPG4.Controllers
{
    public class CatalogController : Controller
    {
        private ProductDal _productDal;

        public CatalogController(ProductDal productDal)
        {
            _productDal = productDal;
        }


		public IActionResult Index(string searchString)
		{
			List<Product> products = _productDal.GetAllProducts();


            // Search using the search bar
			if (!string.IsNullOrEmpty(searchString))
			{
				products = products.Where(p => p.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
			}
			return View(products);
        }

		public IActionResult ProductView(int id)
        {
            Product product = _productDal.GetAllProducts().FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); 
            }
            return View(product);
        }
    }
}
