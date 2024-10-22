using System.Collections.Generic;
using System;

namespace AMAPG4.Models.ContactForm
{
    public interface IContactService : IDisposable
    {              
        List<Contact> GetAllContacts();
        int CreateContact(string name, string firstName, string email, string phoneNumber, string message, ContactStatus status);
        Contact GetContactById(int id);
        void DeleteContact(int id);
        void UpdateContact(Contact contact);
        List<Contact> GetAllPendingContacts();
        List<Contact> GetAllDoneContacts();
    }
}
