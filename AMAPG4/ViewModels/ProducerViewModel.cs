using AMAPG4.Models.User;
using System.Collections.Generic;

namespace AMAPG4.ViewModels
{
    public class ProducerViewModel
    {

        public UserAccount Account { get; set; }
        public Producer Producers { get; set; }

        public List<Producer> ProducersList { get; set; } 
        public int Id { get; set; }
      
     
       
    }
}
