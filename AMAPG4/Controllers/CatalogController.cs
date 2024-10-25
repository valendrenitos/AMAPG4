using AMAPG4.Models;
using AMAPG4.Models.Catalog;
using AMAPG4.Models.Command;
using AMAPG4.Models.User;
using AMAPG4.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AMAPG4.Controllers
{

	public class CatalogController : Microsoft.AspNetCore.Mvc.Controller
	{
		public MyDBContext _bddContext;
		private ProductDal _productDal;

		public CatalogController()
		{
			_productDal = new ProductDal();
		}

		public IActionResult Index(string searchString, string sortOrder, bool? showAll)
		{
			List<Product> products = _productDal.GetAllUnitaryProducts();

			// Search using the search bar
			if (!string.IsNullOrEmpty(searchString))
			{
				products = products.Where(p => p.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
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

			// Create the view model
			CatalogViewModel viewModel = new CatalogViewModel
			{
				Products = products,
				IsAuthenticated = HttpContext.User.Identity.IsAuthenticated,
				UserName = HttpContext.User.Identity.Name
			};


			return View("/Views/LaFerme/Catalog/Index.cshtml", viewModel);
		}

		public IActionResult ProductView(int id)
		{
			Product product = _productDal.GetProductById(id);
			if (product == null)
			{
				return NotFound();
			}

			// Create a view model for the product details
			ProductDetailViewModel productViewModel = new ProductDetailViewModel
			{
				IsAuthenticated = HttpContext.User.Identity.IsAuthenticated,

				ProductName = product.ProductName,
				Price = product.Price,
				Description = product.Description,
				IsAvailable = product.IsAvailable,
				Stock =product.Stock,
				status = StatusType.Waiting,
				ImagePath = product.ImagePath
			};

			return View("/Views/LaFerme/Catalog/ProductView.cshtml", productViewModel);


		}


		[HttpPost]
		public IActionResult ProductView(ProductDetailViewModel productView)
		{

			int quantity = (productView.Quantity );
			;
			Console.Write(quantity);
			Console.Write(productView.ProductName);
			
			Product product = _productDal.GetProductByName(productView.ProductName);
			Console.Write(product.Id);
			int id = product.Id;
			

			UserAccountViewModel UserAccountViewModel =
		new UserAccountViewModel();
			using (UserAccountDal userAccountDal = new UserAccountDal())
			{
				UserAccountViewModel.UserAccount = userAccountDal.GetUserAccount(HttpContext.User.Identity.Name);
			}
			using (OrderLineDal orderLineDal = new OrderLineDal())
			{
				orderLineDal.CheckOrderLine(UserAccountViewModel.UserAccount.Id, quantity, id);
			}

			ProductDetailViewModel productViewModel = new ProductDetailViewModel
			{
				ProductName = product.ProductName,
				Price = product.Price,
				Description = product.Description,
				IsAvailable = product.IsAvailable,
				Stock = product.Stock,
				status = StatusType.Success,


                IsAuthenticated = HttpContext.User.Identity.IsAuthenticated,
				ImagePath = product.ImagePath
            };


			return View("/Views/LaFerme/Catalog/ProductView.cshtml", productViewModel);
		}


    }
}
