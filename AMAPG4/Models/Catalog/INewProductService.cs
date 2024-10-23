using System.Collections.Generic;
using System;

namespace AMAPG4.Models.Catalog
{
    public interface INewProductService : IDisposable
    {
                
        List<NewProduct> GetAllNewProducts();
        int CreateNewProduct(string productName, string description, bool isAvailable, decimal price, int stock, DateTime limitDate, ProductType productType, SubmissionStatus status, int producerId, string imagePath);
    }
}

