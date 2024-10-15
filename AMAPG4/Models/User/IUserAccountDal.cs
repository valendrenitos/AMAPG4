using System;
using System.Collections.Generic;

namespace AMAPG4.Models.User
{
    public interface IUserAccountDal : IDisposable
    {

  
        List<UserAccount> GetAllUserAccounts();
        int CreateUserAccount(string address, string email, string phone,  string name, string password);
        UserAccount Authentication(string email, string password);
        UserAccount GetUserAccount(int id);
        UserAccount GetUserAccount(string idStr);

  
    }
}

