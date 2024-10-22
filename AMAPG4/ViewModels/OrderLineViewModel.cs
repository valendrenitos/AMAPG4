
﻿using AMAPG4.Models.Command;
using AMAPG4.Models.Catalog;
using System.Collections.Generic;

namespace AMAPG4.ViewModels
{
    public class OrderLineViewModel
    {

        public int UserActualId { get; set; }
         public OrderLine orderLine { get; set; } 
        public List<OrderLine> OrderLinesTotal { get; set; }
        public List<OrderLine> OrderLinesCurrent { get; set; }
        public List<string> OrderLinesCurrentList { get; set; }
        public Product product { get; set; }
        public int NewQuantity { get; set; }
        public int OrderLineId { get; set; }
        public decimal Total { get; set; }
    }
    }
    


