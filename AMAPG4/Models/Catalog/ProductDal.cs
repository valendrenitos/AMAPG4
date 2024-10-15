using AMAPG4.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace AMAPG4.Models.Catalog
{
    public class ProductDal
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
            CreateProduct("Miel de Lavande", "Miel doux et floral, idéal pour les tisanes.", true, 8.50m, 100, DateTime.Now.AddMonths(6), ProductType.NonPerishable);
            CreateProduct("Œufs de Poules Heureuses", "Œufs bio issus de poules élevées en plein air.", true, 3.20m, 50, DateTime.Now.AddMonths(3), ProductType.NonPerishable); 
            CreateProduct("Panier de Légumes de Saison", "Un mélange frais de légumes de saison, cultivés localement.", true, 15.00m, 30, DateTime.Now.AddDays(7), ProductType.Basket);

        }

        public List<Product> GetAllProducts()
        {
            return _bddContext.Products.ToList();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        //CRUD

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


        public void UpdateProduct(int id, string productName, string description, bool isAvailable, decimal price, int stock, DateTime limitDate, ProductType productType)
        {
            Product product = _bddContext.Products.Find(id);
            if (product != null)
            {
                product.ProductName = productName;
                product.Description = description;
                product.IsAvailable = isAvailable;
                product.Price = price;
                product.Stock = stock;
                product.LimitDate = limitDate;
                product.ProductType = productType;
                _bddContext.SaveChanges();
            }
        }

    }
}
