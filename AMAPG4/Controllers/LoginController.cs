using AMAPG4.Models.User;
using AMAPG4.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace AMAPG4.Controllers
{
    public class LoginController : Controller
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
                return View(UserAccountViewModel);
            }
            return View(UserAccountViewModel);
        }
        [HttpPost]
        public IActionResult Index(UserAccountViewModel viewModel, string returnUrl)
        {
           
       
            //if (ModelState.IsValid)
            //{
              
               
            
                using (UserAccountDal userAccountDal = new UserAccountDal())
                {   //On vérifie qu'un utilisateur avec ce Nom + MDP existe en allant le chercher dans la BDD
                    UserAccount userAccount =
                        userAccountDal.Authentication(viewModel.UserAccount.Email, viewModel.UserAccount.Password);

                    if (userAccount != null)
                    {
                        List<Claim> userClaims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name, userAccount.Id.ToString())

                        };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(userClaims, "User Identity");
                        ClaimsPrincipal userPrincipal = new ClaimsPrincipal(new[] { claimsIdentity });

                        HttpContext.SignInAsync(userPrincipal);

                    }
                    else
                    {
                        ModelState.AddModelError("UserAccount.Email", "Email et/ou mot de passe incorrect(s)");

                    }

                }
                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }


                return Redirect("/");
            //}
            return View(viewModel);
        }

        public IActionResult CreateAccount()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAccount(UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                using (UserAccountDal userAccountDal = new UserAccountDal())
                {
                    int id = userAccountDal.AddUserAccount(userAccount.Email, userAccount.Address, userAccount.Phone, userAccount.Name, userAccount.Password);

                    return Redirect("/Login/Index");
                }
            }
            return View();
        }

        public IActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
