using AMAPG4.Models;
using AMAPG4.Models.Command;

namespace AMAPG4.ViewModels
{
    public class PaymentViewModel
    {
  
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardNum { get; set; }
        public bool IsConfirmed { get; set; }
        public CommandLine CommandLine { get; set; }
        public int CommandId { get; set; }
        public int SecretCode { get; set; }
        public StatusType status { get; set; }
    }
}
