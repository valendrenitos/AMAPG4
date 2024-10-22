using System;

namespace AMAPG4.Models.ContactForm
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public UserType userType { get; set; }
        public DateTime DateSent { get; set; }
        public ContactStatus Status { get; set; } = ContactStatus.Pending; // Par défaut à "Non traité"
    }

    

    

}
