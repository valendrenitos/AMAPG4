using Microsoft.AspNetCore.Mvc;
using AMAPG4.Models.User;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace AMAPG4.Controllers
{
    [Authorize (Roles ="Admin")]
    public class IndividualController : Controller
    {
        private readonly IndividualDal _individualDal;

        public IndividualController()
        {
            _individualDal = new IndividualDal();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();         }

        
        [HttpPost]
        public IActionResult Create(Individual model)
        {
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
                int newId = _individualDal.CreateIndividual(model.FirstName, DateTime.Now, model.IsContributionPaid, model.IsVolunteer,
                                                model.Account.Email, model.Account.Password, model.Account.Name,
                                                model.Account.Address, model.Account.Phone, model.Account.Role);

                return RedirectToAction("Read", "Individual", new { id = newId });
            }
            return View(model);
        }
               
        [HttpGet]
        public IActionResult Read(int id)
        {
            Individual individual = _individualDal.GetIndividualById(id);
            if (individual == null)
            {
                return NotFound(); 
            }
            return View(individual);  
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Individual individual = _individualDal.GetIndividualById(id);
            if (individual == null)
            {
                return NotFound();
            }
            return View(individual);
        }

        [HttpPost]
        public IActionResult Update(Individual model)
        {
            ModelState.Remove("Account.Password");

            if (ModelState.IsValid)
            {
                Individual individual = _individualDal.GetIndividualById(model.Id);

                if (individual != null)
                {
                    individual.FirstName = model.FirstName;
                    individual.IsContributionPaid = model.IsContributionPaid;
                    individual.IsVolunteer = model.IsVolunteer;
                    individual.Account.Address = model.Account.Address;
                    individual.Account.Email = model.Account.Email;
                    individual.Account.Phone = model.Account.Phone;
                    individual.Account.Name = model.Account.Name;
                    individual.Account.Role = model.Account.Role;

                    _individualDal.UpdateIndividual(individual);
                }
            }
            return RedirectToAction("Read", "Individual", new { id = model.Id });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _individualDal.DeleteIndividual(id);
            return RedirectToAction("Individuals", "Dashboard");
        }
    }
}
