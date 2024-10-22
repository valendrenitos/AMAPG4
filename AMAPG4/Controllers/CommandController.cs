using AMAPG4.Models.Command;
using AMAPG4.Models.User;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AMAPG4.ViewModels;
using System.Xml.Schema;
using System;
using System.Linq;
using AMAPG4.Models;

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
        public IActionResult Payment(CommandViewModel commandViewModel)
        {
            // INTEGRER UNE API ICI
            
        Console.Write(commandViewModel.CommandId);
            
            OrderLineDal orderLineDal = new OrderLineDal();
    

            using (UserAccountDal userAccountDal = new UserAccountDal())
            {
                commandViewModel.UserId = userAccountDal.GetUserAccount(HttpContext.User.Identity.Name).Id;

            }
            commandViewModel.ListOrderline = orderLineDal.GetCurrentOrderLines(commandViewModel.UserId, OrderLineType.Reserved);

            foreach (OrderLine orderline in commandViewModel.ListOrderline)
            {
                commandViewModel.CommandId = orderline.CommandId;
            }
            PaymentViewModel paymentViewModel = new PaymentViewModel();
            using (CommandLineService commandLineService = new CommandLineService())
            {
                paymentViewModel.CommandLine = commandLineService.GetAllCommandLines().FirstOrDefault(c => c.CommandId == commandViewModel.CommandId);
            }
            paymentViewModel.IsConfirmed = false;
            paymentViewModel.status = StatusType.Waiting;

            return View(paymentViewModel);
        }
        [HttpPost]
        public IActionResult Payment(PaymentViewModel paymentViewModel)
        {
            // INTEGRER UNE API ICI
         
            if (paymentViewModel.CardNum == "1111111111111111") // Numero de carte pour EXPEMPLE SEULEMENT
            {
                Console.WriteLine(paymentViewModel.CommandId);
                using (CommandLineService commandLineService = new CommandLineService())
                {
                    
                    commandLineService.UpdateCommandLine(paymentViewModel.CommandId, CommandLineType.Paid);
                    paymentViewModel.CommandLine = commandLineService.GetAllCommandLines().FirstOrDefault(c => c.CommandId == paymentViewModel.CommandId);
                    paymentViewModel.status = StatusType.Success;
                    
                    Console.Write(55);
                }
            }
            else
            {
                using (CommandLineService commandLineService = new CommandLineService())
                {
                    paymentViewModel.CommandLine = commandLineService.GetAllCommandLines().FirstOrDefault(c => c.CommandId == paymentViewModel.CommandId);
                }
                paymentViewModel.status = StatusType.Failed;
           
            }

          

            return View(paymentViewModel);
        }
    }
}
