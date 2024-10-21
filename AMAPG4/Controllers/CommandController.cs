using AMAPG4.Models.Command;
using AMAPG4.Models.User;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AMAPG4.ViewModels;
using System.Xml.Schema;
using System;

namespace AMAPG4.Controllers
{
    public class CommandController : Controller
    {
        public IActionResult Index()
        {
            OrderLineDal orderLineDal = new OrderLineDal();
            CommandViewModel CommandViewModel = new CommandViewModel();

            using (UserAccountDal userAccountDal = new UserAccountDal())
            {
                CommandViewModel.UserId = userAccountDal.GetUserAccount(HttpContext.User.Identity.Name).Id;

            }
            CommandViewModel.ListOrderline = orderLineDal.GetCurrentOrderLines(CommandViewModel.UserId, OrderLineType.Reserved);

            decimal total = 0;
            foreach (OrderLine orderline in CommandViewModel.ListOrderline)
            {
                total = total + orderline.Total;
                CommandViewModel.CommandId = orderline.CommandId;
            }

            CommandViewModel.Total = total;
            CommandViewModel.CommandToLookAt = 0;

            return View(CommandViewModel);
        }



        public IActionResult PastOrder()
        {
            CommandViewModel CommandViewModel = new CommandViewModel();
            OrderLineDal orderLineDal = new OrderLineDal();
            using (UserAccountDal userAccountDal = new UserAccountDal())
            {
                CommandViewModel.UserId = userAccountDal.GetUserAccount(HttpContext.User.Identity.Name).Id;

            }
            using (CommandLineService commandLineService = new CommandLineService())
            {
                CommandViewModel.AllFromUser = commandLineService.GetAllCommandFromUser(CommandViewModel.UserId);

            }

         

            return View(CommandViewModel);
        }
        [HttpPost]
        public IActionResult PastOrder(CommandViewModel CommandViewModel)
        {

            OrderLineDal orderLineDal = new OrderLineDal();
            Console.Write(CommandViewModel.CommandToLookAt);
            using (UserAccountDal userAccountDal = new UserAccountDal())
            {
                CommandViewModel.UserId = userAccountDal.GetUserAccount(HttpContext.User.Identity.Name).Id;

            }
            using (CommandLineService commandLineService = new CommandLineService())
            {
                CommandViewModel.AllFromUser = commandLineService.GetAllCommandFromUser(CommandViewModel.UserId);
                CommandViewModel.ListOrderline = commandLineService.GetAllOrderLineFromCommand(CommandViewModel.CommandToLookAt);
            }
            
            decimal total = 0;
            foreach (OrderLine orderline in CommandViewModel.ListOrderline)
            {
                total = total + orderline.Total;
                CommandViewModel.CommandId = orderline.CommandId;
            }
            return View(CommandViewModel);
        }
    }
}
