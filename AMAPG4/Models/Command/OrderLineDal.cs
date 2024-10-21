
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
using Microsoft.EntityFrameworkCore;

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
          
            return _bddContext.OrderLines.Include(od=>od.Product).ToList();
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

        // fonction de création d'orderline dans la base de donnée
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
            product.Stock = product.Stock - quantity;
            _bddContext.OrderLines.Add(orderLine);
            CommandLineService commandLineService = new CommandLineService();
            commandLineService.CreateCommandLine(orderLine.Total, orderLine.CommandId, orderLine.UserAccountId);
            _bddContext.SaveChanges();
            return orderLine.Id;
        }
        // fonction permettant d'update une orderline
        public void UpdateOrderLine(OrderLine orderline, int quantity)
        {



            //OrderLine order = _bddContext.OrderLines.First(od => od.Id == orderline.Id);
            if (quantity > orderline.Product.Stock)
            {
                quantity = orderline.Product.Stock;
                orderline.Quantity = orderline.Quantity + quantity;
                orderline.Total = orderline.Quantity * orderline.Product.Price;
                quantity = 0;
                UpdateStockFromOrder(orderline.Product, quantity);


            }
            else if (orderline.Quantity <= 0)
            {
                quantity = orderline.Product.Stock + orderline.Quantity;
               
                UpdateStockFromOrder(orderline.Product, quantity);
                _bddContext.Remove(orderline);

            }
            else
            {
				orderline.Quantity = orderline.Quantity + quantity;
                quantity= orderline.Product.Stock - quantity;
                orderline.Total = orderline.Quantity * orderline.Product.Price;
                UpdateStockFromOrder(orderline.Product, quantity);
			}
    
			_bddContext.SaveChanges();


		}


        // Fonction permettant de récupérer un numéro de commande ou d'en générer un
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
            CreateOrderLine(1, 2, 8);
            CreateOrderLine(6, 1, 3);
            CreateOrderLine(3, 1, 4);
        }
        // fonction rattacher au bouton d'ajout d'un produit
         public void CheckOrderLine(int useraccountId, int quantity, int productid)
        {
            OrderLine orderLine = _bddContext.OrderLines.Include(od => od.Product).FirstOrDefault(orderline =>
                (orderline.orderLineType == OrderLineType.Reserved) && (orderline.UserAccountId == useraccountId) && (orderline.Product.Id == productid));
            if (orderLine == null)
            {
                CreateOrderLine(useraccountId, quantity, productid);
            }
            else
            {
                Console.Write(orderLine.Product.Stock);
                UpdateOrderLine(orderLine, quantity);
            }

        }
        public void UpdateStockFromOrder(Product product, int quantity)
        {
            ProductDal productDal = new ProductDal();
            productDal.UpdateProduct(product.Id, product.ProductName, product.Description, product.IsAvailable, product.Price, quantity, product.LimitDate, product.ProductType);
        }
        public void UpdateQuantityFromCart(OrderLine orderline, int quantity)
        {
            int diff = quantity- orderline.Quantity;
            if (diff > orderline.Product.Stock)
            {
                quantity = orderline.Product.Stock;
                orderline.Quantity = orderline.Quantity + quantity;
                orderline.Total = orderline.Quantity * orderline.Product.Price;
                quantity = 0;
                UpdateStockFromOrder(orderline.Product, quantity);
            }
            else if (quantity == 0)
            {
                _bddContext.Remove(orderline);
            }
            else
            {
                orderline.Quantity = quantity;
                quantity= orderline.Product.Stock-diff;
                orderline.Total = orderline.Quantity * orderline.Product.Price;
                UpdateStockFromOrder(orderline.Product, quantity);
            }
            _bddContext.SaveChanges();  
        }
        public OrderLine GetOrderLineById(int id)
        {
            return _bddContext.OrderLines.Include(od => od.Product).FirstOrDefault(orderline =>
                (orderline.Id == id));
        }

} }
