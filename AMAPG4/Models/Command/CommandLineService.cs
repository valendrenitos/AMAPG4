
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using XAct.Collections;

namespace AMAPG4.Models.Command
{
    public class CommandLineService : ICommandLineService
    {
        public MyDBContext _bddContext;
        public CommandLineService()
        {
            _bddContext = new MyDBContext();
        }

        public List<CommandLine> GetAllCommandLines()
        {
            return _bddContext.CommandLines.ToList();
        }
        public CommandLine GetCommandFromId(int id)
        {
            return _bddContext.CommandLines.FirstOrDefault(c =>c.CommandId == id);
        }
        public void Dispose()
        {

            _bddContext.Dispose();

        }
        public void CreateCommandLine(decimal Total, int CommandId, int UserId)
        {
            CommandLine command = _bddContext.CommandLines.FirstOrDefault(c => c.CommandId == CommandId);
            if (command == null)
            {
                CommandLine commandLine = new CommandLine()

                {
                    Total = Total,
                    UserId = UserId,
                    CommandType = CommandLineType.In_Progress,
                    CommandId = CommandId,
                    DateTimeOrdered = DateTime.Now
                };
                _bddContext.CommandLines.Add(commandLine);
            }
            else
            {
                command.Total = command.Total + Total;
            }
            
            _bddContext.SaveChanges();
          
        }
        public void UpdateCommandLine(int CommandId, CommandLineType commandLineType)
        {
            CommandLine command = _bddContext.CommandLines.FirstOrDefault(c => c.CommandId == CommandId);
            TimeSpan time = new TimeSpan(1, 0, 0, 0);
            if (command.CommandType == CommandLineType.In_Progress && (DateTime.Now - command.DateTimeOrdered ) > time)
            {
                DeleteCommandLine(command);
            }
            else if (command.DateTimeDelivered == DateTime.Now && command.CommandType == CommandLineType.Paid)
            {
                command.CommandType = CommandLineType.Delivered;
            }
            else if (command.CommandType == CommandLineType.In_Progress && commandLineType == CommandLineType.Paid)
            {
                command.CommandType = CommandLineType.Paid;
                command.DateTimeDelivered = GetDeliveryDate(command);
                using (OrderLineDal orderLineDal = new OrderLineDal())
                {
                    List <OrderLine> orders = GetAllOrderLineFromCommand(command.CommandId);
                    foreach (OrderLine orderLine in orders)
                    {
                        orderLine.orderLineType = OrderLineType.Paid;
                        orderLine.DateTime= DateTime.Now;
                    }
                }
            }
            _bddContext.SaveChanges();
            
        }
        public void DeleteCommandLine(CommandLine command)
        {
            List<OrderLine> CommandOrder = GetAllOrderLineFromCommand(command.CommandId);
            foreach (OrderLine line in CommandOrder )
                using(OrderLineDal orderline = new OrderLineDal())
                {
                   orderline.UpdateOrderLine(line, 0);
                }
            _bddContext.Remove(command);
            _bddContext.SaveChanges();

        }
        public List<CommandLine> GetAllCommandFromUser(int UserId)
        {
         

            return _bddContext.CommandLines.Where(c=> c.UserId == UserId).ToList();
        }
        public List<OrderLine> GetAllOrderLineFromCommand(int CommandId)
        {
            return _bddContext.OrderLines.Include(od => od.Product).Where(o => o.CommandId == CommandId).ToList();
        }
        public DateTime GetDeliveryDate(CommandLine command)
        {
            DateTime DateDelivery = new DateTime();
            int day= (int)command.DateTimeOrdered.DayOfWeek;
            if (day > 3)
            {
                TimeSpan DayToWait = new TimeSpan((12 - day), 0, 0, 0);
                DateDelivery = command.DateTimeOrdered + DayToWait;
            }
            else
            {
                TimeSpan DayToWait = new TimeSpan((5 - day), 0, 0, 0);
                 DateDelivery = command.DateTimeOrdered + DayToWait;
            }
            return DateDelivery;
        }
        public void UpdateAllCommandLine()
        {
            List<CommandLine> ListCommandLine = GetAllCommandLines();
            foreach (CommandLine line in ListCommandLine)
            {
                UpdateCommandLine(line.CommandId, CommandLineType.In_Progress);
            }
        }
        public void UpdateTotal(int CommandId)
        {
            CommandLine command = _bddContext.CommandLines.FirstOrDefault(c => c.CommandId == CommandId);
            List<OrderLine> CommandOrder = GetAllOrderLineFromCommand(CommandId);
            decimal total = 0;
            foreach (OrderLine line in CommandOrder)
            {
                total += line.Total;
            }
            command.Total = total;
            _bddContext.SaveChanges();
        }

    }
}
