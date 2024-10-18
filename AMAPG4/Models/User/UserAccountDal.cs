using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;
using XSystem.Security.Cryptography;

namespace AMAPG4.Models.User
{
    public class UserAccountDal : IUserAccountDal
    {
   
        public MyDBContext _bddContext;
        public UserAccountDal()
        {
            _bddContext = new MyDBContext();
        }

        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        public void InitializeDataBase()
        {
            UpdateUserRole(1, Role.Admin);
            UpdateUserRole(2, Role.Manager);
        }

        public List<UserAccount> GetAllUserAccounts()
        {
            return _bddContext.UserAccounts.ToList();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public void UpdateUserAccount(int id, string address, string email, string phone, string name, string password)
        {
            UserAccount userAccount = _bddContext.UserAccounts.Find(id);

            if (userAccount != null)
            {
                userAccount.Address = address;
                userAccount.Email = email;
                userAccount.Phone = phone;
                userAccount.Name = name;
                userAccount.Password = password;
                _bddContext.SaveChanges();
            }
        }
        public Role GetUserRole(int userAccountId)
        {
            UserAccount userAccount = _bddContext.UserAccounts.FirstOrDefault(u => u.Id == userAccountId);
            if (userAccount != null)
            {
                return userAccount.Role;
            }
            else
            {
                return Role.Utilisateur; // Retourne 'Utilisateur' par défaut si l'utilisateur n'existe pas
            }
        }
        public void UpdateUserRole(int userAccountId, Role newRole)
        {
            UserAccount userAccount = _bddContext.UserAccounts.FirstOrDefault(u => u.Id == userAccountId);
            if (userAccount != null)
            {
                userAccount.Role = newRole; // Mise à jour du rôle
                _bddContext.SaveChanges();  // Sauvegarde les modifications dans la base de données
            }
        }



        /******************************************************/
        /*          Méthodes pour l'authentification          */
        /******************************************************/

        public UserAccount Authentication(string email, string password)
        {
            string encodedPassword = EncodeMD5(password);
            UserAccount userAccount = this._bddContext.UserAccounts.
                FirstOrDefault(userAccount => (userAccount.Email == email) && (userAccount.Password == encodedPassword));
            return userAccount;
        }

        public UserAccount GetUserAccount(int id)
        {
            return this._bddContext.UserAccounts.Find(id);
        }

        public UserAccount GetUserAccount(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetUserAccount(id);
            }
            return null;
        }

        private string EncodeMD5(string encodedPassword)
        {
            string encodedPasswordSalt = "TP_Authentification" + encodedPassword + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(encodedPasswordSalt)));
        }
    }
}