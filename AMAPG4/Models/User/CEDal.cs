using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace AMAPG4.Models.User
{
    public class CEDal : ICEDal
    {
        private MyDBContext _bddContext;
        private UserAccountDal _userAccountDal;

        public CEDal()
        {
            _bddContext = new MyDBContext();
            _userAccountDal = new UserAccountDal();
        }

        // Récupère toutes les entreprises (CE)
        public List<CE> GetAllCEs()
        {
            return _bddContext.CEs.ToList();
        }

        // Récupère un CE par son Id
        public CE GetCEById(int id)
        {
            return _bddContext.CEs.FirstOrDefault(c => c.Id == id);
        }

        // Ajoute un nouveau CE avec son compte utilisateur associé
        public int CreateCE(string contactName, int numberOfEmployees, bool isContributionPaid,
                  string email, string password, string name, string address, string phone)
        {
            // 1. Crée d'abord le compte utilisateur associé
            int userAccountId = _userAccountDal.CreateUserAccount(address, email, phone, name, password);
            
            // 2. Crée le CE et l'associe au compte utilisateur
            CE ce = new CE()
            {
                ContactName = contactName,
                NumberOfEmployees = numberOfEmployees,
                IsContributionPaid = isContributionPaid,
                AccountId = userAccountId
            };

            // 3. Ajoute le CE à la base de données
            _bddContext.CEs.Add(ce);
            _bddContext.SaveChanges();

            return ce.Id;
        }


        // Met à jour un CE existant et son compte utilisateur associé
        public void UpdateCE(int ceId, string contactName, int numberOfEmployees, bool isContributionPaid, UserAccount account)
        {
            // Mettre à jour le CE
            CE ce = _bddContext.CEs.FirstOrDefault(c => c.Id == ceId);
            if (ce != null)
            {
                ce.ContactName = contactName;
                ce.NumberOfEmployees = numberOfEmployees;
                ce.IsContributionPaid = isContributionPaid;
                _bddContext.SaveChanges();

                // Mettre à jour le compte utilisateur associé
                if (_userAccountDal.GetUserAccount(ce.AccountId) != null)
                {
                    _userAccountDal.UpdateUserAccount(ce.AccountId, account.Address, account.Email, account.Phone, account.Name, account.Password);
                }
            }
        }

        //// Supprime un CE et son compte utilisateur associé
        //public void DeleteCE(int id)
        //{
        //    var ce = _bddContext.CEs.FirstOrDefault(c => c.Id == id);
        //    if (ce != null)
        //    {
        //        // Supprime le compte utilisateur associé
        //        if (ce.Account != null)
        //        {
        //            _userAccountDal.DeleteUserAccount(ce.Account.Id);
        //        }

        //        // Supprime le CE
        //        _bddContext.CEs.Remove(ce);
        //        _bddContext.SaveChanges();
        //    }
        //}

        // Libère les ressources
        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public CE GetCEByUserAccount(int userAccountId)
        {
            return _bddContext.CEs.FirstOrDefault(ce => ce.AccountId == userAccountId);
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

