using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace AMAPG4.Models.User
{
    public class ProducerDal : IProducerDal
    {
        private MyDBContext _bddContext;
        private UserAccountDal _userAccountDal;

        public ProducerDal()
        {
            _bddContext = new MyDBContext();
            _userAccountDal = new UserAccountDal();
        }

        // Récupère tous les producteurs
        public List<Producer> GetAllProducers()
        {
            return _bddContext.Producers.ToList();
        }

        // Récupère un producteur par son Id
        public Producer GetProducerById(int id)
        {
            return _bddContext.Producers.FirstOrDefault(p => p.Id == id);
        }

        // Ajoute un nouveau producteur avec son compte utilisateur associé
        public int CreateProducer(string siret, string contactName, string rib,
                             string email, string password, string name, string address, string phone)
        {
            // 1. Crée d'abord le compte utilisateur associé
            int userAccountId = _userAccountDal.CreateUserAccount(address, email, phone, name, password);
            var userAccount = _userAccountDal.GetUserAccount(userAccountId);

            // 2. Crée le producteur et l'associe au compte utilisateur
            Producer producer = new Producer()
            {
                Siret = siret,
                ContactName = contactName,
                RIB = rib,
                Account = userAccount
            };

            // 3. Ajoute le producteur à la base de données
            _bddContext.Producers.Add(producer);
            _bddContext.SaveChanges();

            return producer.Id;
        }


        // Met à jour un producteur existant et son compte utilisateur associé
        public void UpdateProducer(int producerId, string siret, string contactName, string rib, UserAccount account)
        {
            // Mettre à jour le producteur
            var producer = _bddContext.Producers.FirstOrDefault(p => p.Id == producerId);
            if (producer != null)
            {
                producer.Siret = siret;
                producer.ContactName = contactName;
                producer.RIB = rib;
                _bddContext.SaveChanges();

                // Mettre à jour le compte utilisateur associé
                if (producer.Account != null)
                {
                    _userAccountDal.UpdateUserAccount(producer.Account.Id, account.Address, account.Email, account.Phone, account.Name, account.Password);
                }
            }
        }

        //// Supprime un producteur et son compte utilisateur associé
        //public void DeleteProducer(int id)
        //{
        //    var producer = _bddContext.Producers.FirstOrDefault(p => p.Id == id);
        //    if (producer != null)
        //    {
        //        // Supprime le compte utilisateur associé
        //        if (producer.Account != null)
        //        {
        //            _userAccountDal.DeleteUserAccount(producer.Account.Id);
        //        }

        //        // Supprime le producteur
        //        _bddContext.Producers.Remove(producer);
        //        _bddContext.SaveChanges();
        //    }
        //}

        // Libère les ressources
        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public void Initialize()
        {
            // Exemples de producteurs
            CreateProducer("12345678901234", "Pierre Durand", "FR7630006000011234567890189", "pierre.durand@agricole.com", "DurandPassword123", "Pierre Durand", "30 Chemin des Champs, 84000 Avignon", "0654321987");
            CreateProducer("23456789012345", "Anne Dubois", "FR7630006000011234567890178", "anne.dubois@bio.com", "DuboisPassword456", "Anne Dubois", "40 Rue de la Ferme, 68000 Colmar", "0665432198");
            CreateProducer("34567890123456", "Julien Lefevre", "FR7630006000011234567890167", "julien.lefevre@vin.com", "LefevrePassword789", "Julien Lefevre", "50 Route des Vins, 67000 Strasbourg", "0676543219");
            CreateProducer("45678901234567", "Sophie Morel", "FR7630006000011234567890156", "sophie.morel@produits.com", "MorelPassword321", "Sophie Morel", "60 Chemin des Producteurs, 44000 Nantes", "0687654321");
        }

    }

}