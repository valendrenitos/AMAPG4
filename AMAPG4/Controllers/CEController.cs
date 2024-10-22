using Microsoft.AspNetCore.Mvc;
using AMAPG4.Models.User;

namespace AMAPG4.Controllers
{
    public class CEController : Controller
    {
        private readonly CEDal _ceDal;

        public CEController()
        {
            _ceDal = new CEDal();
        }

        // Action pour afficher le formulaire de mise à jour d'un CE
        [HttpGet]
        public IActionResult Update(int id)
        {
            CE ce = _ceDal.GetCEById(id);
            if (ce == null)
            {
                return NotFound();
            }

            return View(ce);
        }

        // Action pour mettre à jour un CE
        [HttpPost]
        public IActionResult Update(CE model)
        {
            ModelState.Remove("Account.Password");

            if (ModelState.IsValid)
            {
                CE ce = _ceDal.GetCEById(model.Id);

                if (ce != null)
                {
                    ce.ContactName = model.ContactName;
                    ce.NumberOfEmployees = model.NumberOfEmployees;
                    ce.IsContributionPaid = model.IsContributionPaid;
                    ce.Account.Address = model.Account.Address;
                    ce.Account.Email = model.Account.Email;
                    ce.Account.Phone = model.Account.Phone;
                    ce.Account.Name = model.Account.Name;

                    _ceDal.UpdateCE(ce);
                }
            }
            return RedirectToAction("Index", "Dashboard");
        }

        // Action pour supprimer un CE
        [HttpPost]
        public IActionResult Delete(int id)
        {
            CE ce = _ceDal.GetCEById(id);
            if (ce == null)
            {
                return NotFound();
            }
            _ceDal.DeleteCE(ce.Id);
            return RedirectToAction("Index", "Dashboard");
        }

        // Action pour créer un nouveau CE
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CE model)
        {
            if (ModelState.IsValid)
            {
                int newId = _ceDal.CreateCE(
                    model.ContactName,
                    model.NumberOfEmployees,
                    model.IsContributionPaid,
                    model.Account.Email,
                    model.Account.Password,
                    model.Account.Name,
                    model.Account.Address,
                    model.Account.Phone);

                return RedirectToAction("Read", new { id = newId });
            }
            return View(model);
        }

        // Action pour lire les détails d'un CE
        [HttpGet]
        public IActionResult Read(int id)
        {
            CE ce = _ceDal.GetCEById(id);
            if (ce == null)
            {
                return NotFound();
            }

            return View(ce);
        }
    }
}
