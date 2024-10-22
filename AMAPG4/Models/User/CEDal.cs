using AMAPG4.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMAPG4.Models.User
{
    public class CEDal : ICEDal
    {
        private MyDBContext _bddContext;

        public CEDal()
        {
            _bddContext = new MyDBContext();
        }

        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        // Récupère tous les CEs
        public List<CE> GetAllCEs()
        {
            return _bddContext.CEs.Include(c => c.Account).ToList();
        }

        // Récupère un CE par son Id
        public CE GetCEById(int id)
        {
            return GetAllCEs().FirstOrDefault(c => c.Id == id);
        }

        // Récupère un CE par son Account.Id
        public CE GetCEByUserAccount(int userAccountId)
        {
            return GetAllCEs().FirstOrDefault(c => c.Account.Id == userAccountId);
        }

        // Ajoute un nouveau CE avec son compte utilisateur associé
        public int CreateCE(string contactName, int numberOfEmployees, bool isContributionPaid, string email, string password, string name, string address, string phone, Role role = Role.Utilisateur)
        {
            CE ce = new CE()
            {
                ContactName = contactName,
                NumberOfEmployees = numberOfEmployees,
                IsContributionPaid = isContributionPaid,
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

            // Ajoute le CE à la base de données
            _bddContext.CEs.Add(ce);
            _bddContext.SaveChanges();

            return ce.Id;
        }

        // Méthode pour mettre à jour un CE
        public void UpdateCE(CE ce)
        {
            CE existingCE = GetCEById(ce.Id);
            if (existingCE != null)
            {
                // Mise à jour des informations du CE
                existingCE.ContactName = ce.ContactName;
                existingCE.NumberOfEmployees = ce.NumberOfEmployees;
                existingCE.IsContributionPaid = ce.IsContributionPaid;
                existingCE.Account = ce.Account;

                _bddContext.SaveChanges();
            }
        }

        // Supprime un CE et son UserAccount associé
        public void DeleteCE(int id)
        {
            CE ce = GetCEById(id);
            if (ce != null)
            {
                _bddContext.CEs.Remove(ce);
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
            // Exemples de CE
            CreateCE("Jean Dupont", 250, true, "jean.dupont@entreprise.com", "DupontPassword123!", "Jean Dupont", "10 Rue de l'Entreprise, 75001 Paris", "0612345678");
            CreateCE("Marie Leroy", 150, false, "marie.leroy@entreprise.com", "LeroyPassword456!", "Marie Leroy", "15 Rue des Acacias, 69001 Lyon", "0623456789");
            CreateCE("Luc Martin", 500, true, "luc.martin@entreprise.com", "MartinPassword789!", "Luc Martin", "20 Avenue des Champs, 75008 Paris", "0634567890");
            CreateCE("Clara Bernard", 100, false, "clara.bernard@entreprise.com", "BernardPassword321!", "Clara Bernard", "25 Rue de la République, 69002 Lyon", "0645678901");
        }

    }

}