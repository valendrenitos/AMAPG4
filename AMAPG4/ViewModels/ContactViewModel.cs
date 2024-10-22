using AMAPG4.Models.ContactForm;
using System.Collections.Generic;
namespace AMAPG4.ViewModels
{
    public class ContactViewModel
    {
       public List<Contact> Contacts { get; set; }
      
       public ContactStatus Status { get; set; }

    }
}
