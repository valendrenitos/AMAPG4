using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMAPG4.Models.Command
{
    public class CommandLine
    {

        public int Id { get; set; }
        public int CommandId { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Total { get; set; }
        public int UserId { get; set; }
        public CommandLineType CommandType { get; set; }
        public DateTime DateTimeOrdered { get; set; }
        public DateTime DateTimeDelivered { get; set; }


    }
}
