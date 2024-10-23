using AMAPG4.Models.Catalog;
using AMAPG4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AMAPG4.ViewModels;
using AMAPG4.Models.Command;
using AMAPG4.Models.User;
using System;

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
                status = StatusType.Waiting
            };

            return View("/Views/LaFerme/Activities/Index.cshtml", viewModel);
        }

        [HttpPost]
        public IActionResult Index(ActivitiesViewModel activitiesViewModel)
        {
            int quantity = (activitiesViewModel.Quantity);
            ;
            Console.Write(quantity);
            Console.Write(activitiesViewModel.ProductName);

            Product product = _productDal.GetProductByName(activitiesViewModel.ProductName);
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
                

            };
            List<Product> products = _productDal.GetAllActivityProducts();

            ActivitiesViewModel viewModel = new ActivitiesViewModel
            {
                Products = products,
                status = StatusType.Success
            };

            return View("/Views/LaFerme/Activities/Index.cshtml", viewModel);
        }



    }
}
