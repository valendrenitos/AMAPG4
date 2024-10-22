using AMAPG4.Models.Catalog;
using AMAPG4.Models.ContactForm;
using AMAPG4.Models.User;
using AMAPG4.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AMAPG4.Controllers
{
    public class DashboardController : Microsoft.AspNetCore.Mvc.Controller
    {
      
        public IActionResult Index()
        {
            DashboardViewModel dashboardVM = new DashboardViewModel();
            using (UserAccountDal userAccountDal = new UserAccountDal())
            {
                dashboardVM.UserAccounts = userAccountDal.GetAllUserAccounts();
            }
            using (IndividualDal individualDal = new IndividualDal())
            {
                dashboardVM.Individuals = individualDal.GetAllIndividuals();
            }
            using (ProducerDal producerDal = new ProducerDal())
            {
                dashboardVM.Producers = producerDal.GetAllProducers();
            }
            using (CEDal ceDal = new CEDal())
            {
                dashboardVM.CEs = ceDal.GetAllCEs();
            }
            using (ProductDal productDal = new ProductDal())
            {
                dashboardVM.Products = productDal.GetAllProducts();
            }
            using (ContactService contactService = new ContactService())
            {
                dashboardVM.Contacts = contactService.GetAllContacts();
            }

            return View(dashboardVM);

            
        }

        [HttpPost]
        public IActionResult MarkAsTraite(int id)
        {
            using (ContactService contactService = new ContactService())
            {
                // Récupérer le contact par son ID
                var contact = contactService.GetContactById(id);
                if (contact != null)
                {
                    // Changer l'état du traitement à "Traité"
                    contact.Status = ContactStatus.Traite;
                    contactService.UpdateContact(contact);
                }
            }
            return RedirectToAction("Index");
        }


        public IActionResult Individuals()
        {
            DashboardViewModel dashboardVM = new DashboardViewModel();
            using (IndividualDal individualDal = new IndividualDal())
            {
                dashboardVM.Individuals = individualDal.GetAllIndividuals();
            }
            return View(dashboardVM);
        }
        public IActionResult Producers()
        {
            DashboardViewModel dashboardVM = new DashboardViewModel();
            using (ProducerDal producerDal = new ProducerDal())
            {
                dashboardVM.Producers = producerDal.GetAllProducers();
            }
            return View(dashboardVM);
        }
        public IActionResult CEs()
        {
            DashboardViewModel dashboardVM = new DashboardViewModel();
            using (CEDal ceDal = new CEDal())
            {
                dashboardVM.CEs = ceDal.GetAllCEs();
            }
            return View(dashboardVM);
        }

    }


}
