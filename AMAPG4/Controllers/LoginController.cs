﻿using AMAPG4.Models.User;
using AMAPG4.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View( UserAccountViewModel);
        }
        [HttpPost]
        public IActionResult Index(UserAccountViewModel viewModel, string returnUrl)
        {
            // Liste des champs qu'on veut garder dans le ModelState
            string[] fieldsToKeep = { "UserAccount.Email", "UserAccount.Password" };

            // On supprime tous les autres champs du ModelState
            foreach (string key in ModelState.Keys.ToList())
            {
                if (!fieldsToKeep.Contains(key))
                {
                    ModelState.Remove(key);
                }
            }
            Console.WriteLine("1");
            //On ne vérifie que les champs Email et Password
            if (ModelState.IsValid)
            {
                Console.WriteLine("2");
                using (UserAccountDal userAccountDal = new UserAccountDal())
                {   //On vérifie qu'un utilisateur avec ce Nom + MDP existe en allant le chercher dans la BDD
                    UserAccount userAccount =
                        userAccountDal.Authentication(viewModel.UserAccount.Email, viewModel.UserAccount.Password);
                    Console.WriteLine("3");
                    Console.WriteLine(viewModel.UserAccount.Email +" " + viewModel.UserAccount.Password +" "+ userAccount);
                    if (userAccount != null)
                    {
                        List<Claim> userClaims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name, userAccount.Id.ToString()),
                            new Claim(ClaimTypes.Role, userAccount.Role.ToString())

                        };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(userClaims, "User Identity");
                        ClaimsPrincipal userPrincipal = new ClaimsPrincipal(new[] { claimsIdentity });

                        HttpContext.SignInAsync(userPrincipal);
                        Console.WriteLine("4"); 
                    }
                    else
                    {
                        ModelState.AddModelError("UserAccount.Email", "Email et/ou mot de passe incorrect(s)");
                        Console.WriteLine("5");
                    }
                    Console.WriteLine("6");
                }
                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    Console.WriteLine("7");
                    return Redirect(returnUrl);
                }

                Console.WriteLine("8");
                return Redirect("/");
            }
            Console.WriteLine("9");
            return View(viewModel);
        }

        public IActionResult CreateIndividualAccount()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateIndividualAccount(CreateUserAccountViewModel createUserAccountViewModel)
        {
            Console.WriteLine("1");
            
            foreach (string key in ModelState.Keys.ToList())
            {
                bool isValid = !ModelState[key].Errors.Any();

                if (isValid)
                {
                    Console.WriteLine($"{key}: true");
                }
                else
                {
                    Console.WriteLine($"{key}: false");
                }
            }
                                
                if (ModelState.IsValid)
            {
                Console.WriteLine("2");
                using (IndividualDal individualDal = new IndividualDal())
                {
                    Console.WriteLine("3");
                    // Validation  
                    if (createUserAccountViewModel.UserAccount.Password != createUserAccountViewModel.ConfirmPassword)
                    {
                        ModelState.AddModelError("ConfirmPassword", "Les mots de passe ne correspondent pas.");
                    }
                    Console.WriteLine("4");
                    // Vérifier si l'email existe déjà  
                    //if (_bddContext.UserAccounts.Any(u => u.Email == email))  
                    //{  
                    //throw new ArgumentException("Un utilisateur avec cet email existe déjà.");  
                    //}  

                    int id = individualDal.CreateIndividual(
                        createUserAccountViewModel.Individual.FirstName,
                        DateTime.Now,
                        true,
                        createUserAccountViewModel.Individual.IsVolunteer,
                        createUserAccountViewModel.UserAccount.Email,
                        createUserAccountViewModel.UserAccount.Password,
                        createUserAccountViewModel.UserAccount.Name,
                        createUserAccountViewModel.UserAccount.Address,
                        createUserAccountViewModel.UserAccount.Phone);
                    Console.WriteLine("5");
                    return Redirect("/Login/Index");
                }
            }
            Console.WriteLine("6");
            return View();
        }

        public IActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
