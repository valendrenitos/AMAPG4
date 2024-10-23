using AMAPG4.Models;
using AMAPG4.Models.Catalog;
using System;

namespace AMAPG4.ViewModels
{
	public class ProductDetailViewModel
	{
		public string ProductName { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public bool IsAvailable { get; set; }
		public int Quantity { get; set; }
		public int Stock { get; set; }
        public DateTime LimitDate { get; set; }
		public bool IsAuthenticated { get; set; }
		public ProductType ProductType { get; set; }

        public SubmissionStatus SubmissionStatus { get; set; }
        public StatusType status { get; set; }
    }
}
