using AMAPG4.Models.Catalog;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;

namespace AMAPG4.Models.ContactForm
{
    public class ContactService : IContactService
    {
        private MyDBContext _bddContext;
        public ContactService()
        {
            _bddContext = new MyDBContext();
        }

        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        public void InitializeDataBase()
        {
            //DeleteCreateDatabase();
            CreateContact("Delacoste", "Martin", "martin.delascoste@example.fr", "0706369874", "J'ai une question à vous poser");
            CreateContact("Revillard", "Pierre", "pierre.revillard@example.fr", "0706369885", "Je souhaite m'inscrire en tant que CE");


        }

        public List<Contact> GetAllContacts()
        {
            return _bddContext.Contacts.ToList();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public int CreateContact(string name, string firstName, string email, string phoneNumber, string message)
        {
            Contact contact = new Contact()
            {
                Name = name,
                FirstName = firstName,
                Email = email,
                PhoneNumber = phoneNumber,
                Message = message,
                Status = ContactStatus.NonTraite,
            };
            _bddContext.Contacts.Add(contact);
            _bddContext.SaveChanges();
            return contact.Id;

        }
        
            public Contact GetContactById(int id)
            {
                Contact contact = _bddContext.Contacts.Find(id);
                return contact;
            }


        public void UpdateContact(Contact contact)
        {
            Contact existingContact = _bddContext.Contacts.Find(contact.Id);
            if (existingContact != null)
            {
                existingContact.Status = contact.Status;
                _bddContext.SaveChanges();
            }
        }

        public void DeleteContact(int id)
            {
                Contact contact = _bddContext.Contacts.Find(id);
                if (contact != null)
                {
                    _bddContext.Contacts.Remove(contact);
                    _bddContext.SaveChanges();
                }
            }
        }
}
