using AMAPG4.Models.User;
using Microsoft.AspNetCore.Mvc;
using AMAPG4.ViewModels;
using AMAPG4.Models.Command;
using System.Security.Cryptography.X509Certificates;

namespace AMAPG4.Controllers
{
    public class OrderController : Controller
    {
        public string Index()
        {
            UserAccountViewModel UserAccountViewModel =
                new UserAccountViewModel();
            using (UserAccountDal userAccountDal = new UserAccountDal())
            {
                UserAccountViewModel.UserAccount = userAccountDal.GetUserAccount(HttpContext.User.Identity.Name);
            }
            int UserActualId;
            using (OrderLineDal orderLineDal = new OrderLineDal())
            {
                UserActualId = UserAccountViewModel.UserAccount.Id;
            }
            return ""; // mettre vue du panier et afficher l'id utilisateur ;
        }
    }
}
