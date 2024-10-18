
﻿using AMAPG4.Models.Command;
using AMAPG4.Models.Catalog;
using System.Collections.Generic;

namespace AMAPG4.ViewModels
{
    public class OrderLineViewModel
    {
        public OrderLine OrderLine { get; set; }    
        public int UserActualId { get; set; }
   
        public List<OrderLine> OrderLinesTotal { get; set; }
        public List<OrderLine> OrderLinesCurrent { get; set; }
        public List<string> OrderLinesCurrentList { get; set; }
        public Product product { get; set; }
    }
    }
    


