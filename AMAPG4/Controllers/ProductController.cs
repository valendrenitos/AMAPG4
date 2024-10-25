using AMAPG4.Models.Catalog;
using AMAPG4.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using XAct.Library.Settings;

namespace AMAPG4.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductDal _productDal;

        public ProductController()
        {
            _productDal = new ProductDal();
        }
		[Authorize(Roles = "Admin,Manager")]
		[HttpGet]
        public IActionResult Read(int id)
        {
            Product product = _productDal.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
		[Authorize(Roles = "Admin,Manager")]
		[HttpGet]
        public IActionResult Create()
        {
            using (ProducerDal producerDal = new ProducerDal())
            {
                ViewBag.Producers = producerDal.GetAllProducers();
            }

            return View();
        }
		[Authorize(Roles = "Admin,Manager")]
		[HttpPost]
        public IActionResult Create(Product product, IFormFile ProductImage)
        {
            Console.WriteLine("Début de la méthode Create");

            if (product == null)
            {
                Console.WriteLine("Produit est nul");
                return BadRequest("Produit invalide.");
            }

            // Gestion de l'upload d'image
            string imagePath = null;
            if (ProductImage != null && ProductImage.Length > 0)
            {
                Console.WriteLine($"Nom du fichier : {ProductImage.FileName}");
                Console.WriteLine($"Taille du fichier : {ProductImage.Length}");
                Console.WriteLine($"Type de fichier : {ProductImage.ContentType}");

                var supportedTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
                if (supportedTypes.Contains(ProductImage.ContentType))
                {
                    Console.WriteLine("Type de fichier supporté");
                    var fileName = Path.GetFileName(ProductImage.FileName);
                    imagePath = Path.Combine("wwwroot/images/ProductImages", fileName);

                    try
                    {
                        // Enregistre l'image dans le répertoire défini
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            ProductImage.CopyTo(stream);
                        }

                        // Chemin relatif pour la base de données
                        imagePath = "/images/ProductImages/" + fileName;
                        Console.WriteLine($"Image enregistrée à : {imagePath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erreur lors de la sauvegarde de l'image : {ex.Message}");
                        ModelState.AddModelError("ProductImage", "Erreur lors de l'enregistrement de l'image.");
                    }
                }
                else
                {
                    Console.WriteLine("Type de fichier non supporté");
                    ModelState.AddModelError("ProductImage", "Le fichier doit être une image valide.");
                }
            }
            else
            {
                Console.WriteLine("Aucun fichier d'image fourni ou fichier vide.");
            }

            // Suppression des validations sur les champs du producteur non pertinents
            ModelState.Remove("Producer.RIB");
            ModelState.Remove("Producer.Siret");
            ModelState.Remove("Producer.ContactName");

            // Validation du modèle
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState invalide");
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"Erreur dans {error.Key} : {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                }

                using (ProducerDal producerDal = new ProducerDal())
                {
                    ViewBag.Producers = producerDal.GetAllProducers();
                }

                return View(product);
            }

            // Sauvegarde en base de données
            Console.WriteLine("Enregistrement du produit dans la base de données");
            Console.WriteLine($"ProductType sélectionné : {product.ProductType}");
            using (ProductDal productDal = new ProductDal())
            {
                try
                {
                    product.ImagePath = imagePath; // Associe l'image

                    int newId = productDal.CreateProduct(
                        product.ProductName,
                        product.Description,
                        true,
                        product.Price,
                        product.Stock,
                        product.LimitDate,
                        product.ProductType,
                        product.Producer.Id,  // Utilisation correcte de l'ID du producteur
                        product.ImagePath
                    );

                    Console.WriteLine($"Produit enregistré avec succès, ID : {newId}");
                    return RedirectToAction("Read", "Product", new { id = newId });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de l'enregistrement du produit : {ex.Message}");
                    ModelState.AddModelError("", "Erreur lors de l'enregistrement du produit.");
                }
            }

            Console.WriteLine("Fin de la méthode Create");

            // Si une erreur survient, on réaffiche le formulaire
            using (ProducerDal producerDal = new ProducerDal())
            {
                ViewBag.Producers = producerDal.GetAllProducers();
            }

            return View(product);
        }
		[Authorize(Roles = "Admin,Manager")]
		public IActionResult Update(int id)
        {
            Product product = _productDal.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            // Formatage de la date et du prix
            ViewBag.FormattedLimitDate = product.LimitDate.ToString("yyyy-MM-dd"); // Format requis pour les champs de type date
            ViewBag.FormattedPrice = product.Price.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture); // Format décimal

            return View(product);
        }

		[Authorize(Roles = "Admin,Manager")]
		[HttpPost]
        public IActionResult Update(Product product, IFormFile ProductImage)
        {
            if (product == null)
            {
                return BadRequest("Produit invalide.");
            }

            // Gestion de l'upload d'image
            string imagePath = product.ImagePath; // Conserver l'image actuelle par défaut
            if (ProductImage != null && ProductImage.Length > 0)
            {
                String[] supportedTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
                if (supportedTypes.Contains(ProductImage.ContentType))
                {
                    var fileName = Path.GetFileName(ProductImage.FileName);
                    imagePath = Path.Combine("wwwroot/images/ProductImages", fileName);

                    // Enregistre l'image dans le répertoire défini
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        ProductImage.CopyTo(stream);
                    }

                    // Chemin relatif pour la base de données
                    imagePath = "/images/ProductImages/" + fileName;
                }
                else
                {
                    ModelState.AddModelError("ProductImage", "Le fichier doit être une image valide.");
                }
            }
			

			Console.WriteLine($"ProductType sélectionné : {product.ProductType}");
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState invalide");
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"Erreur dans {error.Key} : {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                }

                using (ProducerDal producerDal = new ProducerDal())
                {
                    ViewBag.Producers = producerDal.GetAllProducers();
                }

                return RedirectToAction("Update", "Product", new { id = product.Id });
            }
            Console.WriteLine($"ProductType sélectionné : {product.ProductType}");
            // Mise à jour en base de données
            using (ProductDal productDal = new ProductDal())
            {
                product.ImagePath = imagePath; // Associe l'image

                productDal.UpdateProduct(
                    product.Id,
                    product.ProductName,
                    product.Description,
                    product.IsAvailable,
                    product.Price,
                    product.Stock,
                    product.LimitDate,
                    product.ProductType,
                    product.ImagePath
                );

                return RedirectToAction("Read", "Product", new { id = product.Id });
            }
        }

		[Authorize(Roles = "Admin,Manager")]
		[HttpPost]
        public IActionResult Delete(int id) 
        {
            Product product = _productDal.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            _productDal.DeleteProduct(id);
            return RedirectToAction("Products", "Dashboard");
        }

    }
}