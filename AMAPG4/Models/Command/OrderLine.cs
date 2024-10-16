using AMAPG4.Models.Catalog;
using AMAPG4.Models.User;


namespace AMAPG4.Models.Command
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public int UserAccountId { get; set; }
        public OrderLineType orderLineType { get; set; }
        public int CommandId { get; set; }
    }
}
