using AMAPG4.Models;
using AMAPG4.Models.Catalog;
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
            return View();
        }

    

        [HttpPost]
        public IActionResult SubmitNewProduct(NewProduct newProduct)
        {
            // Vérifie si le modèle est valide
            if (ModelState.IsValid)
            {
                try
                {
                    newProduct.SubmissionStatus = SubmissionStatus.Pending; // État en attente
                    _newProductService.CreateNewProduct(newProduct.ProductName, newProduct.Description, newProduct.IsAvailable, newProduct.Price, newProduct.Stock, newProduct.LimitDate, newProduct.ProductType, newProduct.SubmissionStatus);

                    _bddContext.NewProducts.Add(newProduct); // Ajouter le produit à la base de données
                    _bddContext.SaveChanges(); // Enregistrer les modifications    

                    // Marquer le produit comme soumis

                    ViewBag.Message = "Votre message a été enregistré avec succès.";

                    return RedirectToAction("Dashboard");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Erreur lors de l'enregistrement du message : " + ex.Message;
                }
            }
            else
            {
                // En cas de modèle invalide, ajoutez un message d'erreur
                ViewBag.Message = "Veuillez corriger les erreurs dans le formulaire.";
            }

            // Retourner à la vue avec le modèle pour réafficher les erreurs
            return View("Index", newProduct);

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
