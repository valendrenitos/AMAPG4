using AMAPG4.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace AMAPG4.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            UserViewModel UserViewModel =
                new UserViewModel() { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            if (UserViewModel.Authentifie)
            {
                using (Dal dal = new Dal())
                {
                    UserViewModel.User = dal.ObtenirUtilisateur(HttpContext.User.Identity.Name);
                }
                return View(UserViewModel);
            }
            return View(UserViewModel);
        }
        [HttpPost]
        public IActionResult Index(UserViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (Dal dal = new Dal())
                {   //On vérifie qu'un utilisateur avec ce Nom + MDP existe en allant le chercher dans la BDD
                    User user =
                        dal.Authentifier(viewModel.User.Prenom, viewModel.User.Password);

                    if (user != null)
                    {
                        List<Claim> userClaims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name, user.Id.ToString()),

                        };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(userClaims, "User Identity");
                        ClaimsPrincipal userPrincipal = new ClaimsPrincipal(new[] { claimsIdentity });

                        HttpContext.SignInAsync(userPrincipal);
                    }
                    else
                    {
                        ModelState.AddModelError("Utilisateur.Prenom", "Prénom et/ou mot de passe incorrect(s)");
                    }

                }
                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return Redirect("/");
            }
            return View(viewModel);
        }

        public IActionResult CreerCompte()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreerCompte(User user)
        {
            if (ModelState.IsValid)
            {
                using (Dal dal = new Dal())
                {
                    int id = dal.AjouterUtilisateur(user.Prenom, user.Password);

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
