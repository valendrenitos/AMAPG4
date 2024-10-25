using AMAPG4.Models;
using AMAPG4.Models.Catalog;
using AMAPG4.Models.User;
using AMAPG4.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;

namespace AMAPG4.Controllers
{
    public class NewProductController : Controller
    {
        public MyDBContext _bddContext;
        private NewProductService _newProductService;

        public NewProductController()
        {
            _newProductService = new NewProductService();
        }
        [Authorize]
        public IActionResult Index()
        {
            NewProductViewModel newProductViewModel = new NewProductViewModel();

			//Vérifier si l'utilisateur est connecté
			if (!HttpContext.User.Identity.IsAuthenticated)
			{
				ViewBag.Message = "Vous devez être connecté en tant que producteur pour pouvoir proposer un produit.";
				return RedirectToAction("Index", "Home");
			}
			// Récupérer l'ID de l'utilisateur connecté
			using (UserAccountDal userAccountDal = new UserAccountDal())
            {
                UserAccount userAccount = userAccountDal.GetUserAccount(HttpContext.User.Identity.Name);

                if (userAccount != null)
                {
                    using (ProducerDal producerDal = new ProducerDal())
                    {
                        Producer producer = producerDal.GetProducerByUserAccount(userAccount.Id);
                        if (producer != null)
                        {
                            newProductViewModel.ProducerId = producer.Id;
                        }
                        else
                        {
                            ViewBag.Message = "Vous devez être connecté en tant que producteur pour pouvoir proposer un produit.";
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    ViewBag.Message = "Vous devez être connecté en tant que producteur pour pouvoir proposer un produit.";
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(newProductViewModel);
        }
		[Authorize(Roles = "Admin,Manager")]
		[HttpGet]
        public IActionResult Read(int id)
        {
            NewProduct product = _newProductService.GetNewProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [Authorize][HttpPost]
        public IActionResult Index(NewProductViewModel newProductVM, IFormFile ProductImage)
        {
			//Vérifier si l'utilisateur est connecté
			if (!HttpContext.User.Identity.IsAuthenticated)
			{
				ViewBag.Message = "Vous devez être connecté en tant que producteur pour pouvoir proposer un produit.";
				return RedirectToAction("Index", "Home");
			}
			// Récupère l'objet NewProduct depuis le ViewModel
			NewProduct newProduct = newProductVM.NewProduct;

            if (ModelState.IsValid)
            {
                // Gestion de l'image du produit
                string imagePath = null;
                if (ProductImage != null && ProductImage.Length > 0)
                {
                    var fileName = Path.GetFileName(ProductImage.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/ProductImages", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ProductImage.CopyTo(stream);
                    }

                    imagePath = "/images/ProductImages/" + fileName;
                }

                // Reste de la création du produit avec l'image
                _newProductService.CreateNewProduct(
                    newProduct.ProductName,
                    newProduct.Description,
                    newProduct.IsAvailable,
                    newProduct.Price,
                    newProduct.Stock,
                    newProduct.LimitDate,
                    newProduct.ProductType,
                    newProduct.SubmissionStatus,
                    newProductVM.ProducerId,
                    imagePath // Ajouter le chemin de l'image au produit
                );

                ViewBag.Message = "Votre demande d'ajout d'un nouveau produit a été envoyée avec succès.";
            }
            else
            {
                ViewBag.Message = "Veuillez corriger les erreurs dans le formulaire.";
            }

            return View(newProductVM);
        }

		[Authorize(Roles = "Admin,Manager")]
		[HttpPost]
        public IActionResult Create(int id)
        {
            NewProduct newProduct = _newProductService.GetNewProductById(id);

            if (newProduct == null)
            {
                return NotFound();
            }

            int newId;
            using (ProductDal productDal = new ProductDal())
            {
                newProduct.IsAvailable = true;
                newId = productDal.CreateProduct(
                    newProduct.ProductName,
                    newProduct.Description,
                    newProduct.IsAvailable,
                    newProduct.Price,
                    newProduct.Stock,
                    newProduct.LimitDate,
                    newProduct.ProductType,
                    newProduct.Producer.Id,
                    newProduct.ImagePath
                    );
            }
            _newProductService.DeleteNewProduct(newProduct.Id);

            return RedirectToAction("Read", "Product", new { id = newId });
        }

		[Authorize(Roles = "Admin,Manager")]
		[HttpPost]
        public IActionResult Refuse(int id)
        {
            _newProductService.UpdateNewProduct(id, SubmissionStatus.Rejected);
            return RedirectToAction("NewProducts", "Dashboard");
        }

		[Authorize(Roles = "Admin,Manager")]
		[HttpPost]
        public IActionResult Delete(int id)
        {
            _newProductService.DeleteNewProduct(id);
            return RedirectToAction("NewProducts", "Dashboard");
        }
    }

}
