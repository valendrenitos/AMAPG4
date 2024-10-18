using AMAPG4.Models.User;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using AMAPG4.Models.Catalog;

namespace AMAPG4.Controllers
{
	public class ProducerController : Controller
	{
		private ProducerDal _producerDal;
        public ProducerController()
        {
            _producerDal = new ProducerDal();
        }
        public IActionResult Index(string searchString)
		{
			List<Producer> producers =_producerDal.GetAllProducers();

			// Search using the search bar
			if (!string.IsNullOrEmpty(searchString))
			{
				producers = producers.Where(p => p.ContactName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
			}
			return View(producers);

		}
		public IActionResult ProducerView(int id)
		{
			Producer producer = _producerDal.GetAllProducers().FirstOrDefault(p => p.Id == id);
			if (producer == null)
			{
				return NotFound();
			}
			return View(producer);
		}
	}
}









	
