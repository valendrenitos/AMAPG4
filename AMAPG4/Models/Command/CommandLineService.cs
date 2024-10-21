using AMAPG4.Models.Catalog;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public void UpdateCommandLine(CommandLine command, CommandLineType commandLineType, decimal Total)
        {
            command.Total = Total;
            command.CommandType = commandLineType;
            _bddContext.SaveChanges();

        }
        public void DeleteCommandLine(CommandLine command)
        {
            _bddContext.Remove(command);
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
 

    }
}
