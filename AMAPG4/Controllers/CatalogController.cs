using AMAPG4.Models.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMAPG4.Controllers
{
    [Authorize(Roles = "Manager")]
    public class CatalogController : Controller
    {
        private ProductDal _productDal;

        public CatalogController()
        {
            _productDal = new ProductDal();
        }


		public IActionResult Index(string searchString, string sortOrder, string[] productTypes, bool? showAll)
		{
			List<Product> products = _productDal.GetAllProducts();


            // Search using the search bar
			if (!string.IsNullOrEmpty(searchString))
			{
				products = products.Where(p => p.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
			}

			// If showAll is not checked, filter by product type
			if (showAll != true && productTypes != null && productTypes.Length > 0)
			{
				products = products.Where(p => productTypes.Contains(p.ProductType.ToString())).ToList();
			}

			// Sorting by price
			switch (sortOrder)
            {
                case "ascending":
                    products = products.OrderBy(p => p.Price).ToList();
                    break;
                case "descending":
                    products = products.OrderByDescending(p => p.Price).ToList();
                    break;
                default:
                    break; // No sorting
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
