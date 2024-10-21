using AMAPG4.Models.User;
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
            CreateProduct("Miel de Fleurs Sauvages", "Miel pur et naturel, récolté dans des fleurs sauvages.", true, 9.00m, 80, DateTime.Now.AddMonths(6), ProductType.Unitary);
            CreateProduct("Yaourt de Brebis", "Yaourt crémeux fait à partir de lait de brebis bio.", true, 2.50m, 100, DateTime.Now.AddMonths(2), ProductType.Unitary);
            CreateProduct("Baguette Traditionnelle", "Pain frais cuit au four à bois, idéal pour accompagner vos repas.", true, 1.20m, 60, DateTime.Now.AddDays(1), ProductType.Unitary);
            CreateProduct("Beurre Fermier", "Beurre doux et crémeux, idéal pour vos recettes.", true, 3.50m, 70, DateTime.Now.AddMonths(3), ProductType.Unitary);
            CreateProduct("Œufs de Poules Heureuses", "Œufs bio issus de poules élevées en plein air.", true, 3.20m, 50, DateTime.Now.AddMonths(3), ProductType.Unitary); // Updated
            CreateProduct("Confiture de Fraises", "Confiture artisanale préparée avec des fraises locales.", true, 5.60m, 80, DateTime.Now.AddMonths(12), ProductType.Unitary); // Updated
            CreateProduct("Jus de Pomme Bio", "Jus de pomme frais, pressé à froid à partir de pommes bio.", true, 2.00m, 50, DateTime.Now.AddMonths(4), ProductType.Unitary);
            CreateProduct("Gnocchis de Pommes de Terre", "Gnocchis faits maison, parfaits pour vos recettes italiennes.", true, 5.00m, 40, DateTime.Now.AddMonths(3), ProductType.Unitary);
            CreateProduct("Fromage de Chèvre Affiné", "Fromage de chèvre fermier affiné à la perfection.", true, 6.00m, 70, DateTime.Now.AddMonths(1), ProductType.Unitary);
            CreateProduct("Pesto de Basilic", "Pesto frais fait maison, idéal pour vos pâtes.", true, 5.50m, 60, DateTime.Now.AddMonths(4), ProductType.Unitary);


            // Basket Products
            CreateProduct("Panier de Légumes de Saison", "Un mélange frais de légumes de saison, cultivés localement.", true, 15.00m, 30, DateTime.Now.AddDays(7), ProductType.Basket);
            CreateProduct("Panier de Légumes de Saison - Taille Grande", "Un grand assortiment de légumes de saison, idéal pour les familles.", true, 25.00m, 20, DateTime.Now.AddDays(7), ProductType.Basket);
            CreateProduct("Panier Mixte de Légumes et Fruits", "Un assortiment équilibré de légumes et fruits de saison.", true, 20.00m, 15, DateTime.Now.AddDays(5), ProductType.Basket);


            // Activity Products
            CreateProduct("Atelier de Fabrication de Savons Naturels", "Apprenez à créer vos propres savons avec des ingrédients bio.", true, 35.00m, 12, DateTime.Now.AddMonths(2), ProductType.Activity);
            CreateProduct("Visite de Jardin Botanique", "Découvrez la diversité des plantes lors d'une visite guidée.", true, 18.00m, 40, DateTime.Now.AddMonths(4), ProductType.Activity);
            CreateProduct("Cours de Jardinage Écologique", "Apprenez les techniques de jardinage respectueuses de l'environnement.", true, 25.00m, 15, DateTime.Now.AddMonths(3), ProductType.Activity);
            CreateProduct("Randonnée au Clair de Lune", "Une randonnée nocturne pour découvrir la nature sous les étoiles.", true, 20.00m, 30, DateTime.Now.AddMonths(1), ProductType.Activity);
            CreateProduct("Cours de Cuisine Végétarienne", "Apprenez à préparer des plats végétariens savoureux et sains.", true, 40.00m, 10, DateTime.Now.AddMonths(2), ProductType.Activity);
            CreateProduct("Retraite de Bien-Être", "Un week-end de détente avec yoga, méditation et nature.", true, 150.00m, 8, DateTime.Now.AddMonths(4), ProductType.Activity);

        }

        public List<Product> GetAllProducts()
        {
            return _bddContext.Products.ToList();
        }

        public List<Product> GetAllUnitaryProducts()
        {
            return _bddContext.Products.Where(p => p.ProductType == ProductType.Unitary).ToList();
        }

        public List<Product> GetAllBasketProducts()
        {
            return _bddContext.Products.Where(p => p.ProductType == ProductType.Basket).ToList();
        }

        public List<Product> GetAllActivityProducts()
        {
            return _bddContext.Products.Where(p => p.ProductType == ProductType.Activity).ToList();
        }




        public void Dispose()
        {
            _bddContext.Dispose();
        }

        //*******************CRUD**********************//

        public int CreateProduct(string productName, string description, bool isAvailable, decimal price, int stock, DateTime limitDate, ProductType productType)
        {
            Product product = new Product()
            {
                ProductName = productName,
                Description = description,
                IsAvailable = isAvailable,
                Price = price,
                Stock = stock,
                LimitDate = limitDate,
                ProductType = productType
            };
            _bddContext.Products.Add(product);
            _bddContext.SaveChanges();
            return product.Id;
        }

        public Product GetProductById(int id)
        {
            Product product = _bddContext.Products.Find(id);
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
            Product product;
            return _bddContext.Products.FirstOrDefault(product=>product.ProductName==name);
        }
    }
}
