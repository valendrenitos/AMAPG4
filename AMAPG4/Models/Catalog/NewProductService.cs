﻿using System.Collections.Generic;
using System.Linq;
using System;
using AMAPG4.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AMAPG4.ViewModels;

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
            CreateNewProduct("Fleurs", "Magnifique", true, 15m, 10, DateTime.Now.AddDays(7), ProductType.Unitary, SubmissionStatus.Pending, 8, "/images/ProductImages/fleurs.jpg");
            CreateNewProduct("Crème de marrons", "Délicieuse crème de marrons faite maison, idéale pour les desserts.", true, 8m, 30, DateTime.Now.AddDays(15), ProductType.Unitary, SubmissionStatus.Pending, 4, "/images/ProductImages/cremes-de-marrons.jpg");

        }

        public List<NewProduct> GetAllNewProducts()
        {
            return _bddContext.NewProducts.Include(n => n.Producer).Include(n => n.Producer.Account).ToList();
        }

        public List<NewProduct> GetAllPendingNewProducts()
        {
            return _bddContext.NewProducts.Where(n => n.SubmissionStatus == SubmissionStatus.Pending).Include(n => n.Producer).Include(n => n.Producer.Account).ToList();
        }
        public List<NewProduct> GetAllRefusedNewProducts()
        {
            return _bddContext.NewProducts.Where(n => n.SubmissionStatus == SubmissionStatus.Rejected).Include(n => n.Producer).Include(n => n.Producer.Account).ToList();
        }
        public void Dispose()
        {
            _bddContext.Dispose();
        }


        //*******************CRUD**********************//

        public int CreateNewProduct(string productName, string description, bool isAvailable, decimal price, int stock, DateTime limitDate, ProductType productType, SubmissionStatus status, int producerId, string imagePath)
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
                ImagePath = imagePath

            };
            _bddContext.NewProducts.Add(newProduct);
            _bddContext.SaveChanges();
            return newProduct.Id;
        }

        public NewProduct GetNewProductById(int id)
        {
            NewProduct newproduct = GetAllNewProducts().FirstOrDefault(n => n.Id == id);
            return newproduct;
        }


        public void UpdateNewProduct(int newProductId, SubmissionStatus status)
        {
            // Récupérer le produit par son ID dans la table NewProduct
            NewProduct newProduct = _bddContext.NewProducts.Find(newProductId);
            if (newProduct != null)
            {
                // Mettre à jour le statut du produit
                newProduct.SubmissionStatus = status;

                // Sauvegarder les modifications
                _bddContext.SaveChanges();
            }
        }


        public void UpdateNewProductProposition(int id, string productName, string description, bool isAvailable, decimal price, int stock, DateTime limitDate, ProductType productType, string? imagePath = null)
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
                if (imagePath != null)
                {
                    newProduct.ImagePath = imagePath;
                }

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
        [HttpPost]
        public void UpdateNewProduct(NewProductViewModel newProductViewModel)
        {
            // Récupérer le produit par son ID dans la table NewProduct
            NewProduct newProduct = _bddContext.NewProducts.FirstOrDefault(o => o.Id == newProductViewModel.NewProduct.Id);
            if (newProduct != null)
            {
                newProduct.ProductName = newProductViewModel.NewProduct.ProductName;
                newProduct.Price = newProductViewModel.NewProduct.Price;
                newProduct.Stock = newProductViewModel.NewProduct.Stock;
                newProduct.Description = newProductViewModel.NewProduct.Description;
                newProduct.LimitDate = newProductViewModel.NewProduct.LimitDate;
                newProduct.ProductType = newProductViewModel.NewProduct.ProductType;
                _bddContext.SaveChanges();


            }

        }

    }

}
