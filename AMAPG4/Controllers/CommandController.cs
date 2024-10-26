using AMAPG4.Models.Command;
using AMAPG4.Models.User;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AMAPG4.ViewModels;
using System.Xml.Schema;
using System;
using System.Linq;
using AMAPG4.Models;
using Microsoft.AspNetCore.Authorization;

namespace AMAPG4.Controllers
{

    public class CommandController : Controller
        
    {
        MyDBContext _bddContext;
        public IActionResult Index()
        {
            OrderLineDal orderLineDal = new OrderLineDal();
            CommandViewModel CommandViewModel = new CommandViewModel();

            using (UserAccountDal userAccountDal = new UserAccountDal())
            {
                CommandViewModel.UserId = userAccountDal.GetUserAccount(HttpContext.User.Identity.Name).Id;

            }
            CommandViewModel.ListOrderline = orderLineDal.GetCurrentOrderLines(CommandViewModel.UserId, OrderLineType.Reserved);
            int CommandId=CommandViewModel.ListOrderline[0].CommandId;
          CommandViewModel.CommandId = CommandId;
        using (CommandLineService commandLine = new CommandLineService())
            {

                commandLine.UpdateTotal(CommandId);
                CommandLine command = commandLine.GetCommandFromId(CommandId);
                CommandViewModel.Total = command.Total;

            }

          
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
                foreach (CommandLine command in CommandViewModel.AllFromUser)
                {
                    commandLineService.UpdateTotal(command.CommandId);
                }

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
            

      
            return View(CommandViewModel);
        }
        public IActionResult Payment(CommandViewModel commandViewModel)
        {
            // INTEGRER UNE API ICI
            
        Console.Write(commandViewModel.CommandId);
            Console.Write(commandViewModel.CommandType);
            OrderLineDal orderLineDal = new OrderLineDal();
			using (UserAccountDal userAccountDal = new UserAccountDal())
			{
				if (userAccountDal.GetUserAccount(HttpContext.User.Identity.Name) != null)
                 {
			
					commandViewModel.UserId = userAccountDal.GetUserAccount(HttpContext.User.Identity.Name).Id;

				}
			}
   
            commandViewModel.ListOrderline = orderLineDal.GetCurrentOrderLines(commandViewModel.UserId, OrderLineType.Reserved);
            if (commandViewModel.ListOrderline == null)
            {
                commandViewModel.ListOrderline = orderLineDal.GetCurrentOrderLines(commandViewModel.UserId, OrderLineType.Contribution);
                if (commandViewModel.ListOrderline == null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            foreach (OrderLine orderline in commandViewModel.ListOrderline)
            {
                commandViewModel.CommandId = orderline.CommandId;
            }
            PaymentViewModel paymentViewModel = new PaymentViewModel();
            using (CommandLineService commandLineService = new CommandLineService())
            {
                paymentViewModel.CommandLine = commandLineService.GetAllCommandLines().FirstOrDefault(c => c.CommandId == commandViewModel.CommandId);
                Console.WriteLine(paymentViewModel.CommandLine.CommandId);
                Console.WriteLine(paymentViewModel.CommandLine.CommandType);
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
                
                using (CommandLineService commandLineService = new CommandLineService())
                {
                    paymentViewModel.CommandLine = commandLineService.GetAllCommandLines().FirstOrDefault(c => c.CommandId == paymentViewModel.CommandId);
                    Console.WriteLine(paymentViewModel.CommandLine.CommandId);
                    Console.WriteLine(paymentViewModel.CommandLine.CommandType);
                    if (paymentViewModel.CommandLine.CommandType == CommandLineType.Contribution)
                    {
                        int id = paymentViewModel.CommandLine.UserId;
                        using (IndividualDal userAccountDal = new IndividualDal())
                        {
                           userAccountDal.UpdateContribition(id);
           
                        }

                    }
                    else
                    {
                        commandLineService.UpdateCommandLine(paymentViewModel.CommandId, CommandLineType.Paid);
                    }
                    
                    paymentViewModel.CommandLine = commandLineService.GetAllCommandLines().FirstOrDefault(c => c.CommandId == paymentViewModel.CommandId);
                    paymentViewModel.status = StatusType.Success;
                    
                  
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
