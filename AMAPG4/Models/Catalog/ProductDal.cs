using AMAPG4.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace AMAPG4.Models.Catalog
{
    public class ProductDal : IProductDal
    {
        private MyDBContext _bddContext;
        public ProductDal()
        {
            _bddContext = new MyDBContext();
        }

        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        public void InitializeDataBase()
        {
            //DeleteCreateDatabase();
            // Unitary Products
            CreateProduct("Miel de Fleurs Sauvages",
    "Ce miel pur et naturel est délicatement récolté à partir des fleurs sauvages de nos prairies...",
    true, 9.00m, 80, DateTime.Now.AddMonths(6),
    ProductType.Unitary, 1, "/images/ProductImages/Miel_de_Fleurs_Sauvages.jpg");

            CreateProduct("Yaourt de Brebis",
                "Notre yaourt crémeux est élaboré à partir de lait de brebis bio...",
                true, 2.50m, 100, DateTime.Now.AddMonths(2),
                ProductType.Unitary, 2, "/images/ProductImages/Yaourt_de_Brebis.jpg");

            CreateProduct("Pain Boule Bio",
                "Ce pain boule bio est préparé à partir de farine biologique de blé complet...",
                true, 3.50m, 40, DateTime.Now.AddDays(3),
                ProductType.Unitary, 3, "/images/ProductImages/Pain_Boule_Bio.jpg");

            CreateProduct("Baguette Traditionnelle",
                "Cette baguette est fraîchement cuite au four à bois...",
                true, 1.20m, 60, DateTime.Now.AddDays(1),
                ProductType.Unitary, 3, "/images/ProductImages/Baguette_Traditionnelle.jpg");

            CreateProduct("Beurre Fermier",
                "Notre beurre doux et crémeux est fabriqué à partir de la crème la plus fraîche...",
                true, 3.50m, 70, DateTime.Now.AddMonths(3),
                ProductType.Unitary, 4, "/images/ProductImages/Beurre_Fermier.jpg");

            CreateProduct("Œufs de Poules Heureuses",
                "Ces œufs bio proviennent de poules élevées en plein air...",
                true, 3.20m, 50, DateTime.Now.AddMonths(3),
                ProductType.Unitary, 1, "/images/ProductImages/Oeufs_de_Poules_Heureuses.jpg");

            CreateProduct("Confiture de Fraises",
                "Cette confiture artisanale est préparée avec des fraises locales...",
                true, 5.60m, 80, DateTime.Now.AddMonths(12),
                ProductType.Unitary, 2, "/images/ProductImages/Confiture_de_Fraises.jpg");

            CreateProduct("Panier de Confitures Bio",
                "Offrez-vous un assortiment gourmand avec ce panier de confitures bio...",
                true, 18.50m, 50, DateTime.Now.AddMonths(12),
                ProductType.Unitary, 2, "/images/ProductImages/Panier_de_Confitures_Bio.jpg");

            CreateProduct("Jus de Pomme Bio",
                "Notre jus de pomme bio est pressé à froid à partir de pommes soigneusement sélectionnées...",
                true, 2.00m, 50, DateTime.Now.AddMonths(4),
                ProductType.Unitary, 1, "/images/ProductImages/Jus_de_Pomme_Bio.jpg");

            CreateProduct("Gnocchis de Pommes de Terre",
                "Ces gnocchis faits maison sont préparés avec des pommes de terre fraîches...",
                true, 5.00m, 40, DateTime.Now.AddMonths(3),
                ProductType.Unitary, 4, "/images/ProductImages/Gnocchis_de_Pommes_de_Terre.jpg");

            CreateProduct("Fromage de Chèvre Affiné",
                "Ce fromage de chèvre fermier est affiné à la perfection...",
                true, 6.00m, 70, DateTime.Now.AddMonths(1),
                ProductType.Unitary, 2, "/images/ProductImages/Fromage_de_Chevre_Affine.jpg");

            CreateProduct("Assortiment de 5 Miels",
                "Découvrez un assortiment de 5 miels artisanaux issus de différentes fleurs et terroirs...",
                true, 45.00m, 30, DateTime.Now.AddMonths(2),
                ProductType.Unitary, 1, "/images/ProductImages/Assortiment_de_5_Miels.jpg");

            CreateProduct("Pesto de Basilic",
                "Notre pesto frais fait maison est préparé à partir de basilic aromatique...",
                true, 5.50m, 60, DateTime.Now.AddMonths(4),
                ProductType.Unitary, 1, "/images/ProductImages/Pesto_de_Basilic.jpg");

            CreateProduct("Tartinade de Pois Chiches",
                "Une délicieuse tartinade crémeuse à base de pois chiches...",
                true, 4.00m, 50, DateTime.Now.AddMonths(6),
                ProductType.Unitary, 1, "/images/ProductImages/Tartinade_de_Pois_Chiches.jpg");

            CreateProduct("Compote de Pommes Maison",
                "Compote de pommes faite maison, sans sucres ajoutés...",
                true, 4.50m, 80, DateTime.Now.AddMonths(8),
                ProductType.Unitary, 1, "/images/ProductImages/Compote_de_Pommes_Maison.jpg");

            CreateProduct("Chips de Légumes",
                "Chips croustillantes faites à partir de légumes frais...",
                true, 3.20m, 60, DateTime.Now.AddMonths(6),
                ProductType.Unitary, 2, "/images/ProductImages/Chips_de_Legumes.jpg");

            // Panier
            CreateProduct("Panier de Légumes de Saison",
                "Un mélange frais de légumes de saison, cultivés localement avec soin...",
                true, 15.00m, 30, DateTime.Now.AddDays(7),
                ProductType.Basket, 4, "/images/ProductImages/panier1.jpg");

            CreateProduct("Panier de Légumes de Saison - Taille Grande",
                "Un grand assortiment de légumes de saison, idéal pour les familles...",
                true, 25.00m, 20, DateTime.Now.AddDays(7),
                ProductType.Basket, 1, "/images/ProductImages/Panier_de_Legumes_de_Saison_Taille_Grande.jpg");

            CreateProduct("Panier de Fruits de Saison",
                "Un assortiment coloré de fruits frais, récoltés localement...",
                true, 12.00m, 25, DateTime.Now.AddDays(7),
                ProductType.Basket, 2, "/images/ProductImages/Panier_de_Fruits_de_Saison.jpg");

            CreateProduct("Panier de Fruits de Saison - Taille Grande",
                "Un grand assortiment coloré de fruits frais, récoltés localement...",
                true, 22.00m, 15, DateTime.Now.AddDays(7),
                ProductType.Basket, 2, "/images/ProductImages/Panier_de_Fruits_de_Saison_Taille_Grande.jpg");

            // Activités
            CreateProduct("Atelier de Fabrication de Savons Naturels",
                "Apprenez à créer vos propres savons avec des ingrédients bio...",
                true, 35.00m, 12, DateTime.Now.AddMonths(2),
                ProductType.Activité, 1, "/images/ProductImages/Atelier_de_Fabrication_de_Savons_Naturels.jpg");

            CreateProduct("Visite de Jardin Botanique",
                "Découvrez la diversité des plantes lors d'une visite guidée...",
                true, 18.00m, 40, DateTime.Now.AddMonths(4),
                ProductType.Activité, 1, "/images/ProductImages/Visite_de_Jardin_Botanique.jpg");

            CreateProduct("Cours de Jardinage Écologique",
                "Apprenez les techniques de jardinage respectueuses de l'environnement...",
                true, 25.00m, 15, DateTime.Now.AddMonths(3),
                ProductType.Activité, 2, "/images/ProductImages/Cours_de_Jardinage_Ecologique.jpg");

            CreateProduct("Randonnée au Clair de Lune",
                "Une randonnée nocturne pour découvrir la nature sous les étoiles...",
                true, 20.00m, 30, DateTime.Now.AddMonths(1),
                ProductType.Activité, 2, "/images/ProductImages/Randonnee_au_Clair_de_Lune.jpg");

            CreateProduct("Cours de Cuisine Végétarienne",
                "Apprenez à préparer des plats végétariens savoureux et sains...",
                true, 40.00m, 10, DateTime.Now.AddMonths(2),
                ProductType.Activité, 2, "/images/ProductImages/Cours_de_Cuisine_Vegetarienne.jpg");

            CreateProduct("Retraite de Bien-Être",
                "Un week-end de détente avec yoga, méditation et nature...",
                true, 150.00m, 8, DateTime.Now.AddMonths(4),
                ProductType.Activité, 1, "/images/ProductImages/Retraite_de_Bien_Etre.jpg");

            CreateProduct("Atelier d'Apiculture",
                "Découvrez le monde fascinant des abeilles et apprenez les bases de l'apiculture...",
                true, 75.00m, 12, DateTime.Now.AddMonths(2),
                ProductType.Activité, 1, "/images/ProductImages/Atelier_d_Apiculture.jpg");

            CreateProduct("Atelier Cosmétiques Naturels et Fabrication de Cire d'Abeille",
                "Découvrez comment fabriquer vos propres cosmétiques naturels et apprenez à utiliser la cire d'abeille...",
                true, 110.00m, 12, DateTime.Now.AddMonths(3),
                ProductType.Activité, 1, "/images/ProductImages/Atelier_Cosmetiques_Naturels_et_Cire_d_Abeille.jpg");




        }

        public List<Product> GetAllProducts()
        {

            return _bddContext.Products.Include(p => p.Producer).ThenInclude(pr => pr.Account).ToList();
            		
		}
       

        public List<Product> GetAllUnitaryProducts()
        {
            return _bddContext.Products.Where(p => p.ProductType == ProductType.Unitary).Include(p => p.Producer).ToList();
        }
       

        public List<Product> GetAllBasketProducts()
        {
            return _bddContext.Products.Where(p => p.ProductType == ProductType.Basket).Include(p => p.Producer).ToList();
        }

        public List<Product> GetAllActivityProducts()
        {
            return _bddContext.Products.Where(p => p.ProductType == ProductType.Activité).Include(p => p.Producer).ToList();
        }




        public void Dispose()
        {
            _bddContext.Dispose();
        }

        //*******************CRUD**********************//

        public int CreateProduct(string productName, string description, bool isAvailable, decimal price, int stock, DateTime limitDate, ProductType productType, int producerId, string imagePath)
        {
            Producer producer = _bddContext.Producers.Include(p => p.Account).FirstOrDefault(p => p.Id == producerId);
            if (producer != null)
            {


                Product product = new Product()
                {
                    ProductName = productName,
                    Description = description,
                    IsAvailable = isAvailable,
                    Price = price,
                    Stock = stock,
                    LimitDate = limitDate,
                    ProductType = productType,
                    Producer = producer,
                    ImagePath = imagePath

                };
                _bddContext.Products.Add(product);
                _bddContext.SaveChanges();
                return product.Id;
            }
            return producer.Id;
        }

        public Product GetProductById(int id)
        {
            Product product = GetAllProducts().FirstOrDefault(p => p.Id == id);
            return product;
        }


        public void UpdateProduct(int id, string productName, string description, bool isAvailable, decimal price, int stock, DateTime limitDate, ProductType productType)
        {
            Product product = _bddContext.Products.Find(id);
            if (product != null)
            {
                product.ProductName = productName;
                product.Description = description;
				product.Stock = stock;
                if (stock != 0)
                {
                    product.IsAvailable = true;
                }
                else
                {
                    product.IsAvailable = false;
                }
				
                product.Price = price;
                
                product.LimitDate = limitDate;
                product.ProductType = productType;
                _bddContext.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            Product product = _bddContext.Products.Find(id);
            if (product != null)
            {
                _bddContext.Products.Remove(product);
                _bddContext.SaveChanges();
            }
        }
        public Product GetProductByName(string name)
        {
            
            return _bddContext.Products.FirstOrDefault(product=>product.ProductName==name);
        }
        public List<Product> GetAllProductByProducer(int producerId)
        {
            return _bddContext.Products.Where(p=>p.Producer.Id==producerId).ToList();
        }
    }
}
