using AMAPG4.Models.Catalog;
using AMAPG4.Models.User;
using AMAPG4.Models.ContactForm;
using System.Collections.Generic;

namespace AMAPG4.ViewModels
{
    public class DashboardViewModel
    {
        public List<UserAccount> UserAccounts { get; set; }
        public List<Individual> Individuals { get; set; }
        public List<CE> CEs { get; set; }
        public List<Producer> Producers { get; set; }
        public List<Product> Products { get; set; }
        public List<NewProduct> NewProducts { get; set; }
        public List<NewProduct> RefusedProducts { get; set; }
        public List<Contact> PendingContacts { get; set; }
        public List<Contact> DoneContacts { get; set; }
    }
}
