using System.ComponentModel.DataAnnotations;

namespace AMAPG4.Models.Catalog
{
    public enum ProductType
    {
        [Display(Name = "Panier")]
        Basket = 1,
        [Display(Name = "Unitaire")]
        Unitary = 2,
        [Display(Name = "Activité")]
        Activité = 3
    }
}
