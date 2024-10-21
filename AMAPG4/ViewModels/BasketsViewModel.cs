using AMAPG4.Models.Catalog;
using System.Collections.Generic;

namespace AMAPG4.ViewModels
{
    public class BasketsViewModel
    {
        public List<Product> Products { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
    }
}
