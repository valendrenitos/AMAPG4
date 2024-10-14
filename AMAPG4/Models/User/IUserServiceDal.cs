using System;
using System.Collections.Generic;

namespace AMAPG4.Models.User
{
    public interface IUserServiceDal : IDisposable
    {

  
        List<UserAccount> GetAllUsers();
        int AddUser(string address, string mail, int phone,  string nom, string password);
        UserAccount Authentication(string nom, string password);
        UserAccount GetUser(int id);
        UserAccount GetUser(string idStr);

  
    }
}

