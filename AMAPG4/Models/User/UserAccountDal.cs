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
        private MyDBContext _bddContext;
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
            DeleteCreateDatabase();
            CreateUserAccount("168 rue de Paris, 73000, Chambery", "janettedupont@gmail.com", "0687956475", "Dupont", "123password");
            CreateUserAccount("15 rue des Acacias, 73000, Chambéry", "marie.leroy@gmail.com", "0698123456", "Leroy", "Marie!Pass2024");
            CreateUserAccount("25 avenue des Monts, 73000, Chambéry", "pierre.durand@hotmail.com", "0678912345", "Durand", "SecurePierre123");
            CreateUserAccount("8 boulevard de la Colline, 73000, Chambéry", "julie.perrin@yahoo.fr", "0654321987", "Perrin", "Julie2024$Safe");
            CreateUserAccount("30 rue du Stade, 73000, Chambéry", "alexandre.bernard@outlook.fr", "0645678901", "Bernard", "AlexPass!456");
            CreateUserAccount("50 rue de la Gare, 73000, Chambéry", "lucie.dumont@gmail.com", "0667123456", "Dumont", "Dumont@Password1");

        }

        public List<UserAccount> GetAllUserAccounts()
        {
            return _bddContext.UserAccounts.ToList();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public int CreateUserAccount(string address, string email, string phone, string name, string password)
        {
            string encodedPassword = EncodeMD5(password);
            UserAccount userAccount = new UserAccount() { Address = address, Email = email, Phone = phone, Name = name, Password = encodedPassword };
            _bddContext.UserAccounts.Add(userAccount);
            _bddContext.SaveChanges();
            return userAccount.Id;
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


        /******************************************************/
        /*          Méthodes pour l'authentification          */
        /******************************************************/
        public int AddUserAccount(string email, string password)
        {
            string encodedPassword = EncodeMD5(password);
            UserAccount userAccount = new UserAccount() { Email = email, Password = encodedPassword };
            this._bddContext.UserAccounts.Add(userAccount);
            this._bddContext.SaveChanges();
            return userAccount.Id;
        }

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
