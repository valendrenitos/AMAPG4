using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;

namespace AMAPG4.Models.User
{
    public class UserServiceDal : IUserServiceDal
    {
        private MyDBContext _bddContext;
        public UserServiceDal()
        {
            _bddContext = new MyDBContext();
        }

        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }



        public List<UserAccount> GetAllUsers()
        {
            return _bddContext.UserAccounts.ToList();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public int AddUser(string address, string email, string phone, string name, string password)
        {
            UserAccount user = new UserAccount() { Address = address,  Email = email , Phone = phone, Name = name , Password = password};
            _bddContext.UserAccounts.Add(user);
            _bddContext.SaveChanges();
            return user.Id;
        }

        public void ModifyUser(int id, string address, string email, string phone, string name, string password)
        {
            UserAccount user = _bddContext.UserAccounts.Find(id);

            if (user != null)
            {
                user.Address = address;
                user.Email = email;
                user.Phone = phone;
                user.Name = name;
                user.Password = password;
                _bddContext.SaveChanges();
            }
        }





        /******************************************************/
        /*          Méthodes pour l'authentification          */
        /******************************************************/
        public int AjouterUtilisateur(string nom, string password)
        {
            string motDePasse = EncodeMD5(password);
            Utilisateur user = new Utilisateur() { Prenom = nom, Password = motDePasse };
            this._bddContext.Utilisateurs.Add(user);
            this._bddContext.SaveChanges();
            return user.Id;
        }

        public Utilisateur Authentifier(string nom, string password)
        {
            string motDePasse = EncodeMD5(password);
            Utilisateur user = this._bddContext.Utilisateurs.
                FirstOrDefault(user => (user.Prenom == nom) && (user.Password == motDePasse));
            return user;
        }

        public Utilisateur ObtenirUtilisateur(int id)
        {
            return this._bddContext.Utilisateurs.Find(id);
        }

        public Utilisateur ObtenirUtilisateur(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.ObtenirUtilisateur(id);
            }
            return null;
        }

        private string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "TP_Authentification" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }
    }
}
