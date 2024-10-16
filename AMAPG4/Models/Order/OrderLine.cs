using AMAPG4.Models.Catalog;
using XAct;

namespace AMAPG4.Models.Order
{
    public class OrderLine
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public  int Quantity { get; set; }
        public float PriceTotal { get; set; } 
    }
}
