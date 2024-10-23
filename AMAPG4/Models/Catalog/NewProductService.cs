﻿using System.Collections.Generic;
using System.Linq;
using System;
using AMAPG4.Models.User;
using Microsoft.EntityFrameworkCore;

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

        public void InitializeDataBase()
        {
            CreateNewProduct("Fleurs", "MAgnifique", true, 15m, 10, DateTime.Now.AddDays(7), ProductType.Unitary, SubmissionStatus.Pending, 1,null);
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

            public int CreateNewProduct(string productName, string description, bool isAvailable, decimal price, int stock, DateTime limitDate, ProductType productType, SubmissionStatus status, int producerId, byte[] photodata)
            {
            Producer producer = _bddContext.Producers.Include(p => p.Account).FirstOrDefault(p => p.Id == producerId);
            NewProduct newProduct = new NewProduct()
            {
                ProductName = productName,
                Description = description,
                IsAvailable = isAvailable,
                Price = price,
                Stock = stock,
                LimitDate = limitDate,
                ProductType = productType,
                SubmissionStatus = status,
                Producer = producer,
                PhotoData = photodata
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


        public void UpdateNewProduct(int newProductId, SubmissionStatus status)
        {
            // Récupérer le produit par son ID dans la table NewProduct
            var newProduct = _bddContext.NewProducts.Find(newProductId);
            if (newProduct != null)
            {
                // Mettre à jour le statut du produit
                newProduct.SubmissionStatus = status;

                // Sauvegarder les modifications
                _bddContext.SaveChanges();
            }
        }

        public void MoveNewProductToProduct(int newProductId)
        {
            // Récupérer le produit de la table NewProduct
            var newProduct = _bddContext.NewProducts.Find(newProductId);
            if (newProduct != null && newProduct.SubmissionStatus == SubmissionStatus.Approved)
            {
                // Créer un nouvel objet Product basé sur les données de NewProduct
                Product product = new Product
                {
                    ProductName = newProduct.ProductName,
                    Description = newProduct.Description,
                    IsAvailable = newProduct.IsAvailable,
                    Price = newProduct.Price,
                    Stock = newProduct.Stock,
                    LimitDate = newProduct.LimitDate,
                    ProductType = newProduct.ProductType,
                   
                };

                // Ajouter le produit à la table Product
                _bddContext.Products.Add(product);

                // Supprimer le produit de la table NewProduct
                _bddContext.NewProducts.Remove(newProduct);

                // Sauvegarder les modifications
                _bddContext.SaveChanges();
            }
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

