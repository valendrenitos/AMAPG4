﻿using Microsoft.AspNetCore.Mvc;
using AMAPG4.Models.ContactForm;
using System.Collections.Generic;
using AMAPG4.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace AMAPG4.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController()
        {
            _contactService = new ContactService();
        }
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Read(int id)
        {
            Contact contact = _contactService.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult PendingContacts()
        {
            ContactViewModel contactVM = new ContactViewModel();
            contactVM.Contacts = _contactService.GetAllPendingContacts();
            contactVM.Status = ContactStatus.Pending;
            return View("Contacts", contactVM);
        }
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult DoneContacts()
        {
            ContactViewModel contactVM = new ContactViewModel();
            contactVM.Contacts = _contactService.GetAllDoneContacts();
            contactVM.Status = ContactStatus.Done;
            return View("Contacts", contactVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Contact model)
        {
            if (ModelState.IsValid)
            {
                int newId = _contactService.CreateContact(model.Name, model.FirstName, model.Email, model.PhoneNumber, model.Message, model.Status);

               
                TempData["SuccessMessage"] = "Votre demande a bien été envoyée.";

                
                return RedirectToAction("Create");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _contactService.DeleteContact(id);
            return RedirectToAction("Contacts", "Dashboard");
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public IActionResult ChangeStatus(int id)
        {
            Contact contact = _contactService.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }
            contact.Status = ContactStatus.Done;
            _contactService.UpdateContact(contact);
            return RedirectToAction("Contacts", "Dashboard");
        }

    }
}
