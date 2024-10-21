using AMAPG4.Models.ContactForm;
using AMAPG4.Models.User;
using AMAPG4.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using XAct.Library.Settings;

namespace AMAPG4.Controllers
{
    public class ContactController : Controller
    {
        private ContactService _contactService;
        public ContactController()
        {
            _contactService = new ContactService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _contactService.CreateContact(model.Contact.Name, model.Contact.FirstName, model.Contact.Email, model.Contact.PhoneNumber, model.Contact.Message, model.Contact.Status);

                    ViewBag.Message = "Votre message a été enregistré avec succès.";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Erreur lors de l'enregistrement du message : " + ex.Message;
                }

                return View("Index");
            }

            return View("Index", model);
        }
    }



}


