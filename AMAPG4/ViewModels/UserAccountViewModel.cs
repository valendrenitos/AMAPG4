using AMAPG4.Models.User;
using System.ComponentModel.DataAnnotations;

namespace AMAPG4.ViewModels
{
    public class UserAccountViewModel
    {
        public UserAccount UserAccount { get; set; }
        public bool Authentication { get; set; }
        public CE CE { get; set; }
        public Individual Individual { get; set; }
        public Producer Producer { get; set; }
         
    }
}
