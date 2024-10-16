using System;
using System.Collections.Generic;
using System.Linq;

namespace AMAPG4.Models.User
{
    public class IndividualDal : IIndividualDal
    {
        private MyDBContext _bddContext;
        private UserAccountDal _userAccountDal;

        // Le constructeur injecte les contextes nécessaires
        public IndividualDal()
        {
            _bddContext = new MyDBContext();
            _userAccountDal = new UserAccountDal(); // Injection directe de la DAL UserAccount
        }

        // Récupère tous les individus
        public List<Individual> GetAllIndividuals()
        {
            return _bddContext.Individuals.ToList();
        }

        // Récupère un individu par son Id
        public Individual GetIndividualById(int id)
        {
            return _bddContext.Individuals.FirstOrDefault(i => i.Id == id);
        }

        // Ajoute un nouvel individu avec son compte utilisateur associé
        public int CreateIndividual(string firstName, DateTime inscriptionDate, bool isContributionPaid, bool isVolunteer,
                                 string email, string password, string name, string address, string phone)
        {
            // 1. Crée d'abord le compte utilisateur associé
            int userAccountId = _userAccountDal.CreateUserAccount(address, email, phone, name, password);
            
            // 2. Crée l'individu et l'associe au compte utilisateur
            Individual individual = new Individual()
            {
                FirstName = firstName,
                InscriptionDate = inscriptionDate,
                IsContributionPaid = isContributionPaid,
                IsVolunteer = isVolunteer,
                AccountId = userAccountId
            };

            // 3. Ajoute l'individu à la base de données
            _bddContext.Individuals.Add(individual);
            _bddContext.SaveChanges();

            return individual.Id;
        }


        // Met à jour un individu existant et son UserAccount associé
        public void UpdateIndividual(int individualId, string firstName, DateTime inscriptionDate, bool isContributionPaid, bool isVolunteer,
                                     UserAccount account)
        {
            // 1. Mettre à jour l'individu
            var individual = _bddContext.Individuals.FirstOrDefault(i => i.Id == individualId);
            if (individual != null)
            {
                individual.FirstName = firstName;
                individual.InscriptionDate = inscriptionDate;
                individual.IsContributionPaid = isContributionPaid;
                individual.IsVolunteer = isVolunteer;

                _bddContext.SaveChanges();

                // 2. Mettre à jour le compte utilisateur associé
                if (_userAccountDal.GetUserAccount(individual.AccountId) != null)
                {
                    _userAccountDal.UpdateUserAccount(individual.AccountId, account.Address, account.Email, account.Phone, account.Name, account.Password);
                }
            }
        }

        //// Supprime un individu et son UserAccount associé
        //public void DeleteIndividual(int id)
        //{
        //    var individual = _bddContext.Individuals.FirstOrDefault(i => i.Id == id);
        //    if (individual != null)
        //    {
        //        // 1. Supprime l'utilisateur associé s'il existe
        //        if (individual.Account != null)
        //        {
        //            _userAccountDal.DeleteUserAccount(individual.Account.Id);
        //        }

        //        // 2. Supprime l'individu
        //        _bddContext.Individuals.Remove(individual);
        //        _bddContext.SaveChanges();
        //    }
        //}

        // Méthode pour libérer les ressources
        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public void Initialize()
        {
            // Exemples d'individus
            CreateIndividual("Alex Dupuis", DateTime.Now.AddYears(-2), true, true, "alex.dupuis@exemple.com", "DupuisPassword123!", "Alex Dupuis", "5 Rue des Fleurs, 75012 Paris", "0611223344");
            CreateIndividual("Emma Lefebvre", DateTime.Now.AddYears(-1), true, false, "emma.lefebvre@exemple.com", "LefebvrePassword456", "Emma Lefebvre", "6 Avenue des Lilas, 34000 Montpellier", "0622334455");
            CreateIndividual("Louis Moreau", DateTime.Now.AddMonths(-6), false, true, "louis.moreau@exemple.com", "MoreauPassword789", "Louis Moreau", "7 Rue des Roses, 69003 Lyon", "0633445566");
            CreateIndividual("Isabelle Dubois", DateTime.Now.AddMonths(-3), false, false, "isabelle.dubois@exemple.com", "DuboisPassword321", "Isabelle Dubois", "8 Chemin des Violettes, 13006 Marseille", "0644556677");
        }

    }

}

