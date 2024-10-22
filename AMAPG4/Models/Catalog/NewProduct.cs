using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMAPG4.Models.Catalog
{
    public class NewProduct
    {

        public int Id { get; set; }
        public string ProductName { get; set; }

        public string Description { get; set; }

        public bool IsAvailable { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public int Stock { get; set; }

        public DateTime LimitDate { get; set; }

        public ProductType ProductType { get; set; }

        public SubmissionStatus SubmissionStatus { get; set; }
    }
}
