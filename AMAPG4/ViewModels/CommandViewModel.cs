using AMAPG4.Models.Command;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data;

namespace AMAPG4.ViewModels
{
    public class CommandViewModel
    {
        public int Id { get; set; }
        public int CommandId { get; set; }
        public decimal Total {  get; set; }
        public int UserId { get; set; }
        public CommandType CommandType { get; set; }
        public DateAndTime DateTimeOrdered{ get; set; }
        public DateAndTime DateTimeDelivered { get; set; }
        public List<OrderLine> ListOrderline { get; set; }
        public List<CommandLine> AllFromUser { get; set; }
        public int CommandToLookAt { get; set; }
    }
}
