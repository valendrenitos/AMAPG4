﻿
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;


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
         
                List<CommandLine> Total = GetAllCommandLines();
                List<CommandLine> AllFromUser = new List<CommandLine>();
                foreach (CommandLine line in Total)
                {
                    if (line.UserId == UserId )
                    {
                        AllFromUser.Add(line);
                    }
         
            }
            return AllFromUser;
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

    }
}
