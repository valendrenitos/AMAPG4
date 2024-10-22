using AMAPG4.Models.Catalog;
using System;
using System.Collections.Generic;

namespace AMAPG4.ViewModels
{
    public class BasketsViewModel
    {
        public List<Product> Products { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
        public DateTime LimitDate { get; set; }

        public ProductType ProductType { get; set; }

        public SubmissionStatus SubmissionStatus { get; set; }
    }
}
