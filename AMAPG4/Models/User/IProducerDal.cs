using System;
using System.Collections.Generic;

namespace AMAPG4.Models.User
{
    public interface IProducerDal : IDisposable
    {
        // Récupère tous les producteurs
        List<Producer> GetAllProducers();

        // Récupère un producteur par son Id
        Producer GetProducerById(int id);

        // Ajoute un nouveau producteur avec son compte utilisateur associé
        int CreateProducer(string siret, string contactName, string description, string productionType, string rib,
                             string email, string password, string name, string address, string phone, Role role);

        // Met à jour un producteur existant et son compte utilisateur associé
        void UpdateProducer(Producer producer);

        //// Supprime un producteur et son compte utilisateur associé
        //void DeleteProducer(int id);
    }
}
