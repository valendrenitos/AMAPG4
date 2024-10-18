
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
        public List<OrderLine> GetAllOrderLines()
        {
          
            return _bddContext.OrderLines.ToList();
        }
        public List<OrderLine> GetPastOrderLines(int IdUtilisateur, OrderLineType Paid)
        {
            List<OrderLine> Total = GetAllOrderLines();
            List<OrderLine> Past = new List<OrderLine>();
            foreach (OrderLine line in Total)
            {
                if (line.UserAccountId == IdUtilisateur && line.orderLineType == Paid)
                {
                    Past.Add(line);
                }
            }

            return Past;
        }
        public List<OrderLine> GetCurrentOrderLines(int IdUtilisateur, OrderLineType Reserved)
        {
            List<OrderLine> Total = GetAllOrderLines();
            List < OrderLine > Current = new List<OrderLine>();
            foreach (OrderLine line in Total)
            {
                if (line.UserAccountId == IdUtilisateur && line.orderLineType == Reserved)
                {
                    Current.Add(line);
                }
            }
            return Current;
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
            int TempCommandId = GenerateCommandNumber(useraccountId);
            OrderLine orderLine = new OrderLine()

            {
                Product = product,
                UserAccountId = useraccountId,
                Quantity = quantity,
                Total =  quantity * product.Price,
                orderLineType = OrderLineType.Reserved,
                CommandId = TempCommandId,
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
            int CommandNumber;
            OrderLine orderLine = new OrderLine();
            // Cas ou une commande est en cours
            if (_bddContext.OrderLines.Contains(new OrderLine { orderLineType = OrderLineType.Reserved , UserAccountId = useraccountId}))
            {
                OrderLine orderline = _bddContext.OrderLines.FirstOrDefault(orderline =>
                (orderline.orderLineType == OrderLineType.Reserved) && (orderline.UserAccountId == useraccountId));
                CommandNumber = orderline.CommandId;
            }
            // Cas ou il n'y a jamais eu de commande
            else if (!_bddContext.OrderLines.Contains(new OrderLine { orderLineType = OrderLineType.Paid, UserAccountId = useraccountId }))
            {
                CommandNumber = useraccountId * 1000000 + 1;
            }
            // cas si la commande est créee et qu'une commande a déja été passé
            else
            {
                OrderLine orderline = _bddContext.OrderLines.OrderByDescending(orderline => orderline.CommandId).FirstOrDefault(orderline =>
                (orderline.orderLineType == OrderLineType.Paid) && (orderline.UserAccountId == useraccountId));
                CommandNumber = orderline.CommandId + 1;
            }
            return CommandNumber;
        }
        public void Initialize()
        {
            CreateOrderLine(1,1 ,4);
            CreateOrderLine(1, 4, 2);
            CreateOrderLine(3, 2, 1);
            CreateOrderLine(1, 2, 1);
            CreateOrderLine(2, 1, 4);
            CreateOrderLine(1, 2, 4);
            CreateOrderLine(6, 1, 3);
            CreateOrderLine(3, 1, 4);
        }
         


} }
