using AMAPG4.Models;
using AMAPG4.Models.Catalog;
using AMAPG4.Models.User;
using AMAPG4.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
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

        public IActionResult Index()
        {
            NewProductViewModel newProductViewModel = new NewProductViewModel();
            return View(newProductViewModel);
        }

    

        [HttpPost]
        public IActionResult Index(NewProductViewModel newProductVM)
        {
           
            NewProduct newProduct = newProductVM.NewProduct;
            
            // Vérifie si le modèle est valide
            if (ModelState.IsValid)
            {
                Producer producer = new Producer();

                   using (UserAccountDal userAccountDal = new UserAccountDal())
                    {
                        producer.Account = userAccountDal.GetUserAccount(HttpContext.User.Identity.Name);
                    }
                   
                    using (ProducerDal producerDal = new ProducerDal())
                    {
                       producer = producerDal.GetProducerByUserAccount(producer.Account.Id);
                    }


                    newProduct.SubmissionStatus = SubmissionStatus.Pending; // État en attente
                    _newProductService.CreateNewProduct(newProduct.ProductName, newProduct.Description, newProduct.IsAvailable, newProduct.Price, newProduct.Stock, newProduct.LimitDate, newProduct.ProductType, newProduct.SubmissionStatus, producer.Id);                

                    // Marquer le produit comme soumis

                    ViewBag.Message = "Votre demande d'ajout d'un nouveau produit a été envoyé avec succès.";

                    //return RedirectToAction("Index", "Home");

            }
            else
            {
                // En cas de modèle invalide, ajoutez un message d'erreur
                ViewBag.Message = "Veuillez corriger les erreurs dans le formulaire.";
            }

            // Retourner à la vue avec le modèle pour réafficher les erreurs
            return View(newProductVM);

        }

        [HttpGet]
        public IActionResult Read(int id)
        {
            NewProduct newProduct = _newProductService.GetNewProductById(id);            
            if (newProduct == null)
            {
                return NotFound();
            }           
            return View(newProduct);
        }


        [HttpPost]
        public IActionResult Create(int id)
        {
            NewProduct newProduct = _newProductService.GetNewProductById(id);
            Console.WriteLine(newProduct.ProductType);
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
                    newProduct.Producer.Id
                    );
            }
            _newProductService.UpdateNewProduct(newProduct.Id, SubmissionStatus.Approved);
                
            return RedirectToAction("Read", "Product", new { id = newId });
        }
        
        [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult RejectNewProduct(int id)
            {
                var newProduct = _bddContext.NewProducts.Find(id);
                if (newProduct != null)
                {
                    newProduct.SubmissionStatus = SubmissionStatus.Rejected; // Marquer comme rejeté
                    _bddContext.SaveChanges(); // Enregistrer les modifications
                }

                return RedirectToAction("Dashboard"); // Retourner au tableau de bord
            }
        }




    }
