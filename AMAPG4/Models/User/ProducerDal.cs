using AMAPG4.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMAPG4.Models.User
{
    public class ProducerDal : IProducerDal
    {
        private MyDBContext _bddContext;

        public ProducerDal()
        {
            _bddContext = new MyDBContext();
        }

        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        // Récupère tous les producteurs
        public List<Producer> GetAllProducers()
        {
            return _bddContext.Producers.Include(p => p.Account).ToList();
        }

        // Récupère un producteur par son Id
        public Producer GetProducerById(int id)
        {
            return GetAllProducers().FirstOrDefault(p => p.Id == id);
        }

        // Récupère un producteur par son Account.Id
        public Producer GetProducerByUserAccount(int userAccountId)
        {
            return GetAllProducers().FirstOrDefault(p => p.Account.Id == userAccountId);
        }

        // Ajoute un nouveau producteur avec son compte utilisateur associé
        public int CreateProducer(string siret, string contactName, string rib, string email, string password, string name, string address, string phone, Role role = Role.Utilisateur)
        {
            Producer producer = new Producer()
            {
                Siret = siret,
                ContactName = contactName,
                RIB = rib,
                Account = new UserAccount()
                {
                    Address = address,
                    Email = email,
                    Phone = phone,
                    Name = name,
                    Password = Encode.EncodeMD5(password),
                    Role = role
                }
            };

            // Ajoute le producteur à la base de données
            _bddContext.Producers.Add(producer);
            _bddContext.SaveChanges();

            return producer.Id;
        }

        // Méthode pour mettre à jour un producteur
        public void UpdateProducer(Producer producer)
        {
            Producer existingProducer = GetProducerById(producer.Id);
            if (existingProducer != null)
            {
                // Mise à jour des informations du producteur
                existingProducer.Siret = producer.Siret;
                existingProducer.ContactName = producer.ContactName;
                existingProducer.RIB = producer.RIB;
                existingProducer.Account = producer.Account;

                _bddContext.SaveChanges();
            }
        }

        // Supprime un producteur et son UserAccount associé
        public void DeleteProducer(int id)
        {
            Producer producer = GetProducerById(id);
            if (producer != null)
            {
                _bddContext.Producers.Remove(producer);
                _bddContext.SaveChanges();
            }
        }

        // Méthode pour libérer les ressources
        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public void Initialize()
        {
            // Exemples de producteurs
            CreateProducer("12345678901234", "Pierre Durand", "FR76300060000112345678", "pierre.durand@agricole.com", "DurandPassword123!", "Pierre Durand", "30 Chemin des Champs, 84000 Avignon", "0654321987");
            CreateProducer("23456789012345", "Anne Dubois", "FR76300060000112345678", "anne.dubois@bio.com", "DuboisPassword456!", "Anne Dubois", "40 Rue de la Ferme, 68000 Colmar", "0665432198");
            CreateProducer("34567890123456", "Julien Lefevre", "FR76300060000112345678", "julien.lefevre@vin.com", "LefevrePassword789!", "Julien Lefevre", "50 Route des Vins, 67000 Strasbourg", "0676543219");
            CreateProducer("45678901234567", "Sophie Morel", "FR76300060000112345678", "sophie.morel@produits.com", "MorelPassword321!", "Sophie Morel", "60 Chemin des Producteurs, 44000 Nantes", "0687654321");
        }

    }
}