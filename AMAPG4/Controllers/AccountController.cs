using AMAPG4.Models.User;
using Microsoft.AspNetCore.Mvc;
using AMAPG4.ViewModels;
using System.Linq;


namespace AMAPG4.Controllers
{
    public class AccountController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            UserAccountViewModel UserAccountViewModel =
                new UserAccountViewModel() { Authentication = HttpContext.User.Identity.IsAuthenticated };
            if (UserAccountViewModel.Authentication)
            {
                using (UserAccountDal userAccountDal = new UserAccountDal())
                {
                    UserAccountViewModel.UserAccount = userAccountDal.GetUserAccount(HttpContext.User.Identity.Name);
                }
                using (IndividualDal individualDal = new IndividualDal())
                {
                    UserAccountViewModel.Individual = individualDal.GetIndividualByUserAccount(UserAccountViewModel.UserAccount.Id);
                }
                using (ProducerDal producerDal = new ProducerDal())
                {
                    UserAccountViewModel.Producer = producerDal.GetProducerByUserAccount(UserAccountViewModel.UserAccount.Id);
                }
                using (CEDal ceDal = new CEDal())
                {
                    UserAccountViewModel.CE = ceDal.GetCEByUserAccount(UserAccountViewModel.UserAccount.Id);
                }
                return View(UserAccountViewModel);
            }
            return View(UserAccountViewModel);
        }
    }
}