using AMAPG4.Models.User;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;

using AMAPG4.ViewModels;
using AMAPG4.Models.Command;
using System.Collections.Generic;
using AMAPG4.Models.Catalog;


namespace AMAPG4.Controllers
{
	public class ProducerController : Controller
	{

		public ProducerDal _producerDal;
        public ProducerController()
        {
            _producerDal = new ProducerDal();
        }

        public IActionResult Index(string searchString)
		{
            ProducerViewModel viewModel = new ProducerViewModel();
            viewModel.ProducersList=_producerDal.GetAllProducers();
             
           
			// Search using the search bar
			if (!string.IsNullOrEmpty(searchString))
			{
                viewModel.ProducersList = viewModel.ProducersList.Where(p => p.ContactName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
			}
			return View(viewModel);

		}
     
		public IActionResult ProducerView(int id)
		{


            Producer producer =_producerDal.GetProducerById(id);
            Console.WriteLine(producer.Id);

           

			if (producer == null)
			{
				return NotFound();
			}
            ProducerViewModel model = new ProducerViewModel
            {
                Producers = producer,
                Id = id,
                
                Account=producer.Account,
            };
            return View(model);
		}


        // Action pour afficher le formulaire de mise à jour d'un producteur
        [HttpGet]
        public IActionResult Update(int id)
        {
            Producer producer = _producerDal.GetProducerById(id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // Action pour mettre à jour un producteur
        [HttpPost]
        public IActionResult Update(Producer model)
        {
            ModelState.Remove("Account.Password");

            if (ModelState.IsValid)
            {
                Producer producer = _producerDal.GetProducerById(model.Id);

                if (producer != null)
                {
                    producer.Siret = model.Siret;
                    producer.ContactName = model.ContactName;
                    producer.Description = model.Description;
                    producer.ProductionType = model.ProductionType;
                    producer.RIB = model.RIB;
                    producer.Account.Address = model.Account.Address;
                    producer.Account.Email = model.Account.Email;
                    producer.Account.Phone = model.Account.Phone;
                    producer.Account.Name = model.Account.Name;

                    _producerDal.UpdateProducer(producer);
                }
            }
            return RedirectToAction("Index", "Dashboard");
        }

        // Action pour supprimer un producteur
        [HttpPost]
        public IActionResult Delete(Producer model)
        {
            Producer producer = _producerDal.GetProducerById(model.Id);
            Console.WriteLine(producer);
            if (producer == null)
            {
                Console.WriteLine("Producer not found");
                return NotFound();
            }
            Console.WriteLine("Producer found");
            _producerDal.DeleteProducer(producer.Id);
            return RedirectToAction("Index", "Dashboard");
        }

        // Action pour créer un nouveau producteur
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Producer model)
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
                int newId = _producerDal.CreateProducer(
                    model.Siret,
                    model.ContactName,
                    model.Description,
                    model.ProductionType,
                    model.RIB,
                    model.Account.Email,
                    model.Account.Password,
                    model.Account.Name,
                    model.Account.Address,
                    model.Account.Phone);

                return RedirectToAction("Read", new { id = newId });
            }
            return View(model);
        }

        // Action pour lire les détails d'un producteur
        [HttpGet]
        public IActionResult Read(int id)
        {
            Producer producer = _producerDal.GetProducerById(id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }
        public IActionResult MyProduct()
        {

            UserAccountViewModel UserAccountViewModel =
        new UserAccountViewModel();
            ProducerViewModel ProducerViewModel = new ProducerViewModel();
            using (UserAccountDal userAccountDal = new UserAccountDal())
            {
                UserAccountViewModel.UserAccount = userAccountDal.GetUserAccount(HttpContext.User.Identity.Name);
            }
            using (ProducerDal producerDal = new ProducerDal())
            {
                Producer producer = producerDal.GetProducerByUserAccount(UserAccountViewModel.UserAccount.Id);
                ProducerViewModel.Producers = producer;
            }
            using (ProductDal productDal = new ProductDal())
            {
                List<Product> products = productDal.GetAllProductByProducer(ProducerViewModel.Producers.Id);
                ProducerViewModel.Products = products;
            }



            return View(ProducerViewModel);
        }

    }
}








