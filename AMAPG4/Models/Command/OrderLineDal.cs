
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Http;

using AMAPG4.ViewModels;
using AMAPG4.Models.Catalog;
using System.Linq;
using XAct;

using AMAPG4.Models.User;

namespace AMAPG4.Models.Command
{
    public class OrderLineDal : IOrderLineDal

    {
        public int CommandNumber;
        public MyDBContext _bddContext;

        public OrderLineDal()
        {
            _bddContext = new MyDBContext();
        }
        public List<OrderLine> GetAllOrderLines(int IdUtilisateur)
        {
            return _bddContext.OrderLines.ToList();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public int GetProduct()
        {
            Product product = new Product();
            int ProductId = product.Id;
            return ProductId;
        }
        public int CreateOrderLine(int useraccountId, int quantity, int productid)
        {
            Product product = _bddContext.Products.Find(productid);

            OrderLine orderLine = new OrderLine()

            {
                ProductId = productid,
                UserAccountId = useraccountId,
                Quantity = quantity,
                Total = quantity * product.Price,
                orderLineType = OrderLineType.Reserved,
                CommandId = GenerateCommandNumber(useraccountId),
            };

            _bddContext.OrderLines.Add(orderLine);
            _bddContext.SaveChanges();
            return orderLine.Id;
        }
        public void UpdateOrderLine(int id, int qantity)
        {
            OrderLine orderline = _bddContext.OrderLines.Find(id);

        }
        public int GenerateCommandNumber(int useraccountId)
        {

            // Cas ou une commande est en cours
            if (_bddContext.OrderLines.Contains(new OrderLine { orderLineType = OrderLineType.Reserved, UserAccountId = useraccountId }))
            {
                OrderLine orderline = _bddContext.OrderLines.FirstOrDefault(orderline =>
                (orderline.orderLineType == OrderLineType.Reserved) && (orderline.UserAccountId == useraccountId));
                int CommandNumber = orderline.CommandId;
            }
            // Cas ou il n'y a jamais eu de commande
            else if (!_bddContext.OrderLines.Contains(new OrderLine { orderLineType = OrderLineType.Paid, UserAccountId = useraccountId }))
            {
                int CommandNumber = useraccountId * 100000000 + 1;
            }
            // cas si la commande est créee et qu'une commande a déja été passé
            else
            {
                OrderLine orderline = _bddContext.OrderLines.OrderByDescending(orderline => orderline.CommandId).FirstOrDefault(orderline =>
                (orderline.orderLineType == OrderLineType.Paid) && (orderline.UserAccountId == useraccountId));
                int CommandNumber = orderline.CommandId + 1;
            }
            return CommandNumber;
        }
    }
}
