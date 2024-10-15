using System;

namespace AMAPG4.Models.Catalog
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }

        public string Description { get; set; }

        public bool IsAvailable { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public DateTime LimitDate {  get; set; }    

        public ProductType ProductType { get; set; }

    }
}
