using AMAPG4.Models.Catalog;
using System.Collections.Generic;
using System;

namespace AMAPG4.Models.ContactForm
{
    public interface IContactService : IDisposable
    {
              
            List<Contact> GetAllContacts();
            int CreateContact(string name, string firstName, string email, string phoneNumber, string message, ContactStatus status);
        

    }
}
