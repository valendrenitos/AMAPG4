
﻿using AMAPG4.Models.Catalog;
using AMAPG4.Models.User;
using System.ComponentModel.DataAnnotations.Schema;


namespace AMAPG4.Models.Command
{
    public class OrderLine
    {
        public int Id { get; set; }
          // [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }
        public int UserAccountId { get; set; }
        public OrderLineType orderLineType { get; set; }
        public int CommandId { get; set; }
    }
}
