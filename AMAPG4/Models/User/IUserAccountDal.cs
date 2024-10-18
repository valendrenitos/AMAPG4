using System;
using System.Collections.Generic;

namespace AMAPG4.Models.User
{
    public interface IUserAccountDal : IDisposable
    {
        List<UserAccount> GetAllUserAccounts();
        UserAccount Authentication(string email, string password);
        UserAccount GetUserAccount(int id);
        UserAccount GetUserAccount(string idStr);
  
    }
}

