using Microsoft.VisualBasic;
using System;

namespace AMAPG4.Models.Command
{
    public class CommandLine
    {
      
            public int Id { get; set; }
            public int CommandId { get; set; }
            public decimal Total { get; set; }
            public int UserId { get; set; }
            public CommandLineType CommandType { get; set; }
            public DateTime DateTimeOrdered { get; set; }
            public DateTime DateTimeDelivered { get; set; }

        
    }
}
