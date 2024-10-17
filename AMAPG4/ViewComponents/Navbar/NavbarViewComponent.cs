using AMAPG4.Models.User;
using AMAPG4.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AMAPG4.ViewComponents.Navbar
{
    public class NavbarViewComponent : ViewComponent
    {
       
        private ProducerDal _producerDal;
        private IndividualDal _individualDal;
        private CEDal _ceDal;

        public NavbarViewComponent()
        {
            _producerDal = new ProducerDal();
            _individualDal = new IndividualDal();
            _ceDal = new CEDal();
        }
                public IViewComponentResult Invoke()
        {
            NavbarViewModel model = new NavbarViewModel();

            // Vérifier si l'utilisateur est authentifié
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                // Récupérer l'userAccountId de l'utilisateur
                int userAccountId = int.Parse(HttpContext.User.Identity.Name);

                // Vérifier les types de comptes liés à l'utilisateur
                model.IsProducer = _producerDal.GetProducerByUserAccount(userAccountId) != null;
                model.IsIndividual = _individualDal.GetIndividualByUserAccount(userAccountId) != null;
                model.IsCE = _ceDal.GetCEByUserAccount(userAccountId) != null;
            }
            return View(model); // Retourne le ViewModel à la vue associée
        }

    }


}
