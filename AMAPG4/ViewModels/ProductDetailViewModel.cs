namespace AMAPG4.ViewModels
{
	public class ProductDetailViewModel
	{
		public string ProductName { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public bool IsAvailable { get; set; }
		public int Quantity { get; set; }
		public int Stock { get; set; }
	}
}
