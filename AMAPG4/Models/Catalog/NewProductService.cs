using System.Collections.Generic;
using System.Linq;
using System;

namespace AMAPG4.Models.Catalog
{
    public class NewProductService : INewProductService
    {

      
        
            private MyDBContext _bddContext;
            public NewProductService()
            {
                _bddContext = new MyDBContext();
            }

            public void DeleteCreateDatabase()
            {
                _bddContext.Database.EnsureDeleted();
                _bddContext.Database.EnsureCreated();
            }

           

            public List<NewProduct> GetAllNewProducts()
            {
                return _bddContext.NewProducts.ToList();
            }

            public void Dispose()
            {
                _bddContext.Dispose();
            }

            //*******************CRUD**********************//

            public int CreateNewProduct(string productName, string description, bool isAvailable, decimal price, int stock, DateTime limitDate, ProductType productType, SubmissionStatus status )
            {
                NewProduct newProduct = new NewProduct()
                {
                    ProductName = productName,
                    Description = description,
                    IsAvailable = isAvailable,
                    Price = price,
                    Stock = stock,
                    LimitDate = limitDate,
                    ProductType = productType,
                    SubmissionStatus = status

                };
                _bddContext.NewProducts.Add(newProduct);
                _bddContext.SaveChanges();
                return newProduct.Id;
            }

            public NewProduct GetNewProductById(int id)
            {
                NewProduct newproduct = _bddContext.NewProducts.Find(id);
                return newproduct;
            }


            public void UpdateNewProduct(int id, string productName, string description, bool isAvailable, decimal price, int stock, DateTime limitDate, ProductType productType, SubmissionStatus status)
            {
                NewProduct newProduct = _bddContext.NewProducts.Find(id);
                if (newProduct != null)
                {
                newProduct.ProductName = productName;
                newProduct.Description = description;
                newProduct.Stock = stock;
                    if (stock != 0)
                    {
                    newProduct.IsAvailable = true;
                    }
                    else
                    {
                    newProduct.IsAvailable = false;
                    }

                newProduct.Price = price;

                newProduct.LimitDate = limitDate;
                newProduct.ProductType = productType;
                newProduct.SubmissionStatus = status;

                    _bddContext.SaveChanges();
                }
            }

            public void DeleteNewProduct(int id)
            {
                NewProduct newProduct = _bddContext.NewProducts.Find(id);
                if (newProduct != null)
                {
                    _bddContext.NewProducts.Remove(newProduct);
                    _bddContext.SaveChanges();
                }
            }
            public NewProduct GetNewProductByName(string name)
            {
                NewProduct newProduct;
                return _bddContext.NewProducts.FirstOrDefault(newProduct => newProduct.ProductName == name);
            }
        }
    }

