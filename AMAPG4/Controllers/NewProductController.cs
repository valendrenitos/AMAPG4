using AMAPG4.Models;
using AMAPG4.Models.Catalog;
using AMAPG4.Models.User;
using AMAPG4.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using XAct.Users;


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
        public IActionResult SaveFile (FileUpload fileObj)
        {
            
            NewProductViewModel newProductViewModel = JsonConvert.DeserializeObject<NewProductViewModel>(fileObj.NewProductViewModel);
            NewProduct newProduct = newProductViewModel.NewProduct;


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
                
                
                if (fileObj.file.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        fileObj.file.CopyTo(ms);
                        byte[] fileBytes = ms.ToArray();
                        newProduct.PhotoData = fileBytes;
                        _newProductService.CreateNewProduct(newProduct.ProductName, newProduct.Description, newProduct.IsAvailable, newProduct.Price, newProduct.Stock, newProduct.LimitDate, newProduct.ProductType, newProduct.SubmissionStatus, producer.Id, newProduct.PhotoData);
                    }
                }
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
                    _newProductService.CreateNewProduct(newProduct.ProductName, newProduct.Description, newProduct.IsAvailable, newProduct.Price, newProduct.Stock, newProduct.LimitDate, newProduct.ProductType, newProduct.SubmissionStatus, producer.Id, null);                

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

        [HttpPost]
        public IActionResult ValidateAndAddProduct(int newProductId)
        {
            // Mettre à jour le statut du produit
            _newProductService.UpdateNewProduct(newProductId, SubmissionStatus.Approved);

            // Transférer le produit vers la table Product
            _newProductService.MoveNewProductToProduct(newProductId);

            // Rediriger vers le catalogue
            return RedirectToAction("Index", "Catalog", "Laferme");
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
