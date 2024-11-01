﻿using AMAPG4.Models.User;
using Microsoft.AspNetCore.Mvc;
using AMAPG4.ViewModels;
using AMAPG4.Models.Command;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using AMAPG4.Models.Catalog;
using System;

namespace AMAPG4.Controllers
{
    public class OrderController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            UserAccountViewModel UserAccountViewModel =
                new UserAccountViewModel();
            OrderLineViewModel OrderLineViewModel =
                new OrderLineViewModel();
            using (UserAccountDal userAccountDal = new UserAccountDal())
            {
                UserAccountViewModel.UserAccount = userAccountDal.GetUserAccount(HttpContext.User.Identity.Name);

            }

            OrderLineDal orderLineDal = new OrderLineDal();

            OrderLineViewModel.UserActualId = UserAccountViewModel.UserAccount.Id;
            OrderLineViewModel.OrderLinesTotal = orderLineDal.GetPastOrderLines(UserAccountViewModel.UserAccount.Id, OrderLineType.Paid);
            OrderLineViewModel.OrderLinesCurrent = orderLineDal.GetCurrentOrderLines(UserAccountViewModel.UserAccount.Id, OrderLineType.Reserved);
            foreach (OrderLine orderLine in OrderLineViewModel.OrderLinesCurrent)
            {
                OrderLineViewModel.Total = orderLine.Total + OrderLineViewModel.Total;
            }


            return View(OrderLineViewModel); // mettre vue du panier et afficher l'id utilisateur ;
        }
        [HttpPost]
        public IActionResult Index(OrderLineViewModel orderLineViewModel)
        {

            OrderLineDal orderLineDal = new OrderLineDal();
            OrderLine orderLine = orderLineDal.GetOrderLineById(orderLineViewModel.OrderLineId);
            orderLineDal.UpdateQuantityFromCart(orderLine, orderLineViewModel.NewQuantity);

            UserAccountViewModel UserAccountViewModel =
                                           new UserAccountViewModel();
            using (UserAccountDal userAccountDal = new UserAccountDal())
            {
                UserAccountViewModel.UserAccount = userAccountDal.GetUserAccount(HttpContext.User.Identity.Name);

            }



            orderLineViewModel.UserActualId = UserAccountViewModel.UserAccount.Id;
            orderLineViewModel.OrderLinesTotal = orderLineDal.GetPastOrderLines(UserAccountViewModel.UserAccount.Id, OrderLineType.Paid);
            orderLineViewModel.OrderLinesCurrent = orderLineDal.GetCurrentOrderLines(UserAccountViewModel.UserAccount.Id, OrderLineType.Reserved);
            //orderLineViewModel1.OrderLine = null;


            // Calcul du total
            foreach (OrderLine orderline in orderLineViewModel.OrderLinesCurrent)
            {
                orderLineViewModel.Total = orderline.Total + orderLineViewModel.Total;
            }




            return View(orderLineViewModel);
        }


    }
}
