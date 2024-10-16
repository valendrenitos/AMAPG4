using System;
using System.Collections.Generic;

namespace AMAPG4.Models.Command
{
    public interface IOrderLineDal : IDisposable
    {
        public List<OrderLine> GetAllOrderLines(int IdUtilisateur);
        public int CreateOrderLine(int useraccountId, int quantity, int productid);

    }
}
