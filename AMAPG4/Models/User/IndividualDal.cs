using AMAPG4.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMAPG4.Models.User
{
    public class IndividualDal : IIndividualDal
    {
        private MyDBContext _bddContext;

        public IndividualDal()
        {
            _bddContext = new MyDBContext();
        }

        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }
        // Récupère tous les individus
        public List<Individual> GetAllIndividuals()
        {
            return _bddContext.Individuals.Include(i => i.Account).ToList();
        }

        // Récupère un individu par son Id
        public Individual GetIndividualById(int id)
        {
            return GetAllIndividuals().FirstOrDefault(i => i.Id == id);
        }

        // Récupère un individu par son Account.Id
        public Individual GetIndividualByUserAccount(int userAccountId)
        {
            return GetAllIndividuals().FirstOrDefault(i => i.Account.Id == userAccountId);
        }

        // Ajoute un nouvel individu avec son compte utilisateur associé
        public int CreateIndividual(string firstName, DateTime inscriptionDate, bool isContributionPaid, bool isVolunteer,
                                    string email, string password, string name, string address, string phone, Role role = Role.Utilisateur)
        {
            Individual individual = new Individual()
            {
                FirstName = firstName,
                InscriptionDate = inscriptionDate,
                IsContributionPaid = isContributionPaid,
                IsVolunteer = isVolunteer,
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

            // Ajoute l'individu à la base de données
            _bddContext.Individuals.Add(individual);
            _bddContext.SaveChanges();

            return individual.Id;
        }



        // Méthode pour mettre à jour un individu
        public void UpdateIndividual(Individual individual)
        {
            Individual existingIndividual = GetIndividualById(individual.Id);
            if (existingIndividual != null)
            {       
                // Mise à jour des informations de l'individu
                existingIndividual.FirstName = individual.FirstName;
                existingIndividual.InscriptionDate = individual.InscriptionDate;
                existingIndividual.IsContributionPaid = individual.IsContributionPaid;
                existingIndividual.IsVolunteer = individual.IsVolunteer;
                existingIndividual.Account = individual.Account;
             
                _bddContext.SaveChanges();
            }
        }

        // Supprime un individu et son UserAccount associé
        public void DeleteIndividual(int id)
        {
            Individual individual = GetIndividualById(id);
            if (individual != null)
            {
                _bddContext.Individuals.Remove(individual);
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
            DeleteCreateDatabase();
            // Exemples d'individus
            CreateIndividual("Alex", DateTime.Now.AddYears(-2), true, true, "alex.dupuis@exemple.com", "DupuisPassword123!", "Dupuis", "5 Rue des Fleurs, 75012 Paris", "0611223344");
            CreateIndividual("Emma", DateTime.Now.AddYears(-1), true, false, "emma.lefebvre@exemple.com", "LefebvrePassword456!", "Lefebvre", "6 Avenue des Lilas, 34000 Montpellier", "0622334455");
            CreateIndividual("Louis", DateTime.Now.AddMonths(-6), false, true, "louis.moreau@exemple.com", "MoreauPassword789!", "Moreau", "7 Rue des Roses, 69003 Lyon", "0633445566");
            CreateIndividual("Isabelle", DateTime.Now.AddMonths(-3), false, false, "isabelle.dubois@exemple.com", "DuboisPassword321!", "Dubois", "8 Chemin des Violettes, 13006 Marseille", "0644556677");
            
        }

    }

}

