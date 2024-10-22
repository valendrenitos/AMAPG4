using System;
using System.Collections.Generic;

namespace AMAPG4.Models.User
{
    public interface ICEDal : IDisposable
    {
        // Récupère toutes les entreprises (CE)
        List<CE> GetAllCEs();

        // Récupère un CE par son Id
        CE GetCEById(int id);

        // Ajoute un nouveau CE avec son compte utilisateur associé
        int CreateCE(string contactName, int numberOfEmployees, bool isContributionPaid,
                  string email, string password, string name, string address, string phone, Role role);

        // Met à jour un CE existant et son compte utilisateur associé
        void UpdateCE(CE ce);

        //// Supprime un CE et son compte utilisateur associé
        //void DeleteCE(int id);
    }
}
