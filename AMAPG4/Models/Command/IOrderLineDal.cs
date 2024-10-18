using System;
using System.Collections.Generic;

namespace AMAPG4.Models.Command
{
    public interface IOrderLineDal : IDisposable
    {
        public List<OrderLine> GetAllOrderLines();
        public int CreateOrderLine(int useraccountId, int quantity, int productid);
        public List<OrderLine> GetCurrentOrderLines(int IdUtilisateur, OrderLineType Reserved);
        public List<OrderLine> GetPastOrderLines(int IdUtilisateur, OrderLineType Paid);
    }
}
