using AMAPG4.Models.Catalog;

namespace AMAPG4.ViewModels
{
    public class NewProductViewModel
    {
        public NewProduct NewProduct {  get; set; }
        public ProductType ProductType { get; set; }

        public int ProducerId { get; set; }
    }
}
