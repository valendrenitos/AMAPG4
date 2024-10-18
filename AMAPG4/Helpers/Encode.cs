using System.Text;
using System;
using XSystem.Security.Cryptography;

namespace AMAPG4.Helpers
{
    public class Encode
    {
        public static string EncodeMD5(string encodedPassword)
        {
            string encodedPasswordSalt = "TP_Authentification" + encodedPassword + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(encodedPasswordSalt)));
        }
    }
}
