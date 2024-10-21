using AMAPG4.Models.Catalog;
using AMAPG4.Models.ContactForm;
using AMAPG4.Models.User;
using AMAPG4.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AMAPG4.Controllers
{
    public class DashboardController : Controller
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
            using (NewProductService newProductService = new NewProductService())
            {
                dashboardVM.NewProducts = newProductService.GetAllNewProducts();
            }

            return View(dashboardVM);

            
        }


        

      


    }


}
