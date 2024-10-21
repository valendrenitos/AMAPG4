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
            CreateProduct("Miel de Lavande", "Miel doux et floral, idéal pour les tisanes.", true, 8.50m, 100, DateTime.Now.AddMonths(6), ProductType.Unitary);
            CreateProduct("Œufs de Poules Heureuses", "Œufs bio issus de poules élevées en plein air.", true, 3.20m, 50, DateTime.Now.AddMonths(3), ProductType.Unitary); 
            CreateProduct("Panier de Légumes de Saison", "Un mélange frais de légumes de saison, cultivés localement.", true, 15.00m, 30, DateTime.Now.AddDays(7), ProductType.Basket);
            CreateProduct("Confiture de Fraises", "Confiture artisanale préparée avec des fraises locales.", true, 5.60m, 80, DateTime.Now.AddMonths(12), ProductType.Unitary);
            CreateProduct("Lait Cru Fermier", "Lait frais directement de la ferme, non pasteurisé.", true, 1.50m, 40, DateTime.Now.AddDays(3), ProductType.Unitary);
            CreateProduct("Baguette Traditionnelle", "Pain frais cuit au four à bois, idéal pour accompagner vos repas.", true, 1.20m, 60, DateTime.Now.AddDays(1), ProductType.Unitary);
            CreateProduct("Panier de Fruits Bio", "Un assortiment de fruits de saison issus de l'agriculture biologique.", true, 12.00m, 25, DateTime.Now.AddDays(5), ProductType.Basket);
            CreateProduct("Fromage de Chèvre Affiné", "Fromage de chèvre fermier affiné à la perfection.", true, 6.00m, 70, DateTime.Now.AddMonths(1), ProductType.Unitary);
            CreateProduct("Atelier de Cuisine Écologique", "Un atelier pour apprendre à cuisiner avec des ingrédients bio et locaux.", true, 30.00m, 15, DateTime.Now.AddMonths(2), ProductType.Activity);
            CreateProduct("Visite de Ferme Bio", "Découvrez les pratiques de l'agriculture biologique lors d'une visite guidée de notre ferme.", true, 20.00m, 50, DateTime.Now.AddMonths(3), ProductType.Activity);
            CreateProduct("Randonnée en Nature", "Une randonnée guidée pour explorer la biodiversité locale et apprendre sur les plantes comestibles.", true, 15.00m, 25, DateTime.Now.AddMonths(1), ProductType.Activity);
            CreateProduct("Séance de Yoga en Plein Air", "Une séance de yoga relaxante dans un cadre naturel, accessible à tous les niveaux.", true, 12.00m, 30, DateTime.Now.AddMonths(1), ProductType.Activity);



        }

        public List<Product> GetAllProducts()
        {
            return _bddContext.Products.ToList();
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
