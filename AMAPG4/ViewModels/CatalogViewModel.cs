using AMAPG4.Models.Catalog;
using System.Collections.Generic;

namespace AMAPG4.ViewModels
{
	public class CatalogViewModel
	{
		public List<Product> Products { get; set; }
		public Product product { get; set; }
		public bool IsAuthenticated { get; set; }
		public string UserName { get; set; }
	}
}