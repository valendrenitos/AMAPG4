using AMAPG4.Models;
using AMAPG4.Models.User;
using AMAPG4.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AMAPG4.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private MyDBContext _bddContext;

        public AccountController()
        {
            _bddContext = new MyDBContext();
        }

        public IActionResult Index()
        {
            UserAccountViewModel userAccountViewModel =
                new UserAccountViewModel() { Authentication = HttpContext.User.Identity.IsAuthenticated };

            if (userAccountViewModel.Authentication)
            {
                using (UserAccountDal userAccountDal = new UserAccountDal())
                {
                    userAccountViewModel.UserAccount = userAccountDal.GetUserAccount(HttpContext.User.Identity.Name);
                }
                using (IndividualDal individualDal = new IndividualDal())
                {
                    userAccountViewModel.Individual = individualDal.GetIndividualByUserAccount(userAccountViewModel.UserAccount.Id);
                }
                using (ProducerDal producerDal = new ProducerDal())
                {
                    userAccountViewModel.Producer = producerDal.GetProducerByUserAccount(userAccountViewModel.UserAccount.Id);
                }
                using (CEDal ceDal = new CEDal())
                {
                    userAccountViewModel.CE = ceDal.GetCEByUserAccount(userAccountViewModel.UserAccount.Id);
                }
            }

            return View(userAccountViewModel);
        }
        [HttpGet]
        public IActionResult UpdateI()
        {
            UserAccountViewModel model = new UserAccountViewModel()
            {
                Authentication = HttpContext.User.Identity.IsAuthenticated
            };
            Console.WriteLine(model.Authentication);
            if (model.Authentication)
            {
               
                using (UserAccountDal userAccountDal = new UserAccountDal())
                {
                    model.UserAccount = userAccountDal.GetUserAccount(HttpContext.User.Identity.Name);
                    Console.WriteLine(model.UserAccount.Id);
                }
                using (IndividualDal individualDal = new IndividualDal())
                {
                    model.Individual = individualDal.GetIndividualByUserAccount(model.UserAccount.Id);
                    Console.WriteLine(model.Individual.Id);
                }
            }

            return View("Update", model);
        }

        [HttpPost]
        public IActionResult UpdateI(UserAccountViewModel model)
        {
                      
                using (IndividualDal individualDal = new IndividualDal())
                {
                    Individual individual = individualDal.GetIndividualByUserAccount(model.UserAccount.Id);
                    individual.Account.Name = model.UserAccount.Name;
                    individual.FirstName = model.Individual.FirstName;
                    individual.Account.Email = model.UserAccount.Email;
                    individual.Account.Address = model.UserAccount.Address;
                    individual.Account.Phone = model.UserAccount.Phone;
                    individual.IsVolunteer = model.Individual.IsVolunteer;
                    individualDal.UpdateIndividual(individual);
                }
        
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateP()
        {
            UserAccountViewModel model = new UserAccountViewModel()
            {
                Authentication = HttpContext.User.Identity.IsAuthenticated
            };

            if (model.Authentication)
            {
                using (UserAccountDal userAccountDal = new UserAccountDal())
                {
                    model.UserAccount = userAccountDal.GetUserAccount(HttpContext.User.Identity.Name);
                }
                using (ProducerDal producerDal = new ProducerDal())
                {
                    model.Producer = producerDal.GetProducerByUserAccount(model.UserAccount.Id);
                }
            }

            return View("Update", model);
        }

        [HttpPost]
        public IActionResult UpdateP(UserAccountViewModel model)
        {
                using (ProducerDal producerDal = new ProducerDal())
                {
                    Producer producer = producerDal.GetProducerByUserAccount(model.UserAccount.Id);
                    producer.Account.Name = model.UserAccount.Name;
                    producer.Account.Email = model.UserAccount.Email;
                    producer.Account.Address = model.UserAccount.Address;
                    producer.Account.Phone = model.UserAccount.Phone;
                    producer.ContactName = model.Producer.ContactName;
                    producer.Siret = model.Producer.Siret;
                    producer.RIB = model.Producer.RIB;
                    producerDal.UpdateProducer(producer);
            }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateCE()
        {
            UserAccountViewModel model = new UserAccountViewModel()
            {
                Authentication = HttpContext.User.Identity.IsAuthenticated
            };

            if (model.Authentication)
            {
                using (UserAccountDal userAccountDal = new UserAccountDal())
                {
                    model.UserAccount = userAccountDal.GetUserAccount(HttpContext.User.Identity.Name);
                }
                using (CEDal ceDal = new CEDal())
                {
                    model.CE = ceDal.GetCEByUserAccount(model.UserAccount.Id);
                }
            }

            return View("Update", model);
        }

        [HttpPost]
        public IActionResult UpdateCE(UserAccountViewModel model)
        {            
                using (CEDal ceDal = new CEDal())
                {
                    CE ce = ceDal.GetCEByUserAccount(model.UserAccount.Id);
                    ce.Account.Name = model.UserAccount.Name;
                    ce.Account.Email = model.UserAccount.Email;
                    ce.Account.Address = model.UserAccount.Address;
                    ce.Account.Phone = model.UserAccount.Phone;
                    ce.ContactName = model.CE.ContactName;
                    ce.NumberOfEmployees = model.CE.NumberOfEmployees;
                    ceDal.UpdateCE(ce);
            }
            
            return RedirectToAction("Index");
        }

    }
}
