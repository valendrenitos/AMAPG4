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
            UtilisateurViewModel utilisateurViewModel =
                new UtilisateurViewModel() { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            if (utilisateurViewModel.Authentifie)
            {
                using (Dal dal = new Dal())
                {
                    utilisateurViewModel.Utilisateur = dal.ObtenirUtilisateur(HttpContext.User.Identity.Name);
                }
                return View(utilisateurViewModel);
            }
            return View(utilisateurViewModel);
        }
        [HttpPost]
        public IActionResult Index(UtilisateurViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (Dal dal = new Dal())
                {   //On vérifie qu'un utilisateur avec ce Nom + MDP existe en allant le chercher dans la BDD
                    Utilisateur utilisateur =
                        dal.Authentifier(viewModel.Utilisateur.Prenom, viewModel.Utilisateur.Password);

                    if (utilisateur != null)
                    {
                        List<Claim> userClaims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name, utilisateur.Id.ToString()),

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
        public IActionResult CreerCompte(Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                using (Dal dal = new Dal())
                {
                    int id = dal.AjouterUtilisateur(utilisateur.Prenom, utilisateur.Password);

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
