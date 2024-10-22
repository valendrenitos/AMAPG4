using AMAPG4.Models.User;
using System.Collections.Generic;
using System;
using AMAPG4.Models.Catalog;

namespace AMAPG4.Models.Catalog
{
    public interface IProductDal : IDisposable
    {
        List<Product> GetAllProducts();
        int CreateProduct(string productName, string description, bool isAvailable, decimal price, int stock, DateTime limitDate, ProductType productType, int producerId);
    }
}
