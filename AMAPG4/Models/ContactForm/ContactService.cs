using System.Collections.Generic;
using System.Linq;
using System;

namespace AMAPG4.Models.ContactForm
{
    public class ContactService : IContactService
    {
        private readonly MyDBContext _bddContext;

        public ContactService()
        {
            _bddContext = new MyDBContext();
        }

        public List<Contact> GetAllContacts()
        {
            return _bddContext.Contacts.ToList();
        }
        public List<Contact> GetAllPendingContacts()
        {
            return _bddContext.Contacts.Where(c => c.Status == ContactStatus.Pending).ToList();
        }
        public List<Contact> GetAllDoneContacts()
        {
            return _bddContext.Contacts.Where(c => c.Status == ContactStatus.Done).ToList();
        }
        public Contact GetContactById(int id)
        {
            return _bddContext.Contacts.Find(id);
        }

        public int CreateContact(string name, string firstName, string email, string phoneNumber, string message, ContactStatus status = ContactStatus.Pending)
        {
            Contact contact = new Contact
            {
                Name = name,
                FirstName = firstName,
                Email = email,
                PhoneNumber = phoneNumber,
                Message = message,
                DateSent = DateTime.Now,
                Status = status
            };

            _bddContext.Contacts.Add(contact);
            _bddContext.SaveChanges();
            return contact.Id;
        }

        public void UpdateContact(Contact contact)
        {
            Contact existingContact = GetContactById(contact.Id);
            if (existingContact != null)
            {
                existingContact.Name = contact.Name;
                existingContact.FirstName = contact.FirstName;
                existingContact.Email = contact.Email;
                existingContact.PhoneNumber = contact.PhoneNumber;
                existingContact.Message = contact.Message;
                existingContact.Status = contact.Status;

                _bddContext.SaveChanges();
            }
        }

        public void DeleteContact(int id)
        {
            Contact contact = GetContactById(id);
            if (contact != null)
            {
                _bddContext.Contacts.Remove(contact);
                _bddContext.SaveChanges();
            }
        }

        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public void InitializeDataBase()
        {
            //DeleteCreateDatabase();
            CreateContact("Delacoste", "Martin", "martin.delascoste@yahoo.fr", "0706369874", "Est-ce que tous vos producteurs sont certifiés biologique ?", ContactStatus.Pending);
            CreateContact("Revillard", "Pierre", "pierre.revillard@msn.com", "0706369885", "Je souhaite m'inscrire en tant que CE, merci de m'indiquer la marche à suivre", ContactStatus.Pending);
            CreateContact("Lefevre", "Sophie", "sophie.lefevre@gmail.com", "0706369812", "Bonjour, je voudrais en savoir plus sur les types de paniers disponibles et leurs prix.", ContactStatus.Pending);
            CreateContact("Martinez", "Julien", "julien.martinez@free.fr", "0706369873", "Est-ce possible d'avoir des produits sans abonnement, uniquement à la demande ?", ContactStatus.Pending);
        }

    }
}