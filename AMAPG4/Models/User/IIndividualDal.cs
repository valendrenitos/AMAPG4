using System;
using System.Collections.Generic;

namespace AMAPG4.Models.User
{
    public interface IIndividualDal : IDisposable
    {
        // Récupère tous les individus
        List<Individual> GetAllIndividuals();

        // Récupère un individu par son Id
        Individual GetIndividualById(int id);

        // Ajoute un nouvel individu avec un compte utilisateur associé
        int CreateIndividual(string firstName, DateTime inscriptionDate, bool isContributionPaid, bool isVolunteer,
                                 string email, string password, string name, string address, string phone, Role role);

        // Met à jour un individu existant et son compte utilisateur associé
        void UpdateIndividual(Individual individual);

        //// Supprime un individu et son compte utilisateur associé
        //void DeleteIndividual(int id);
    }
}
