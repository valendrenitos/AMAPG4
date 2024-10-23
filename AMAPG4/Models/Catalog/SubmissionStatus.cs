using System.ComponentModel.DataAnnotations;

namespace AMAPG4.Models.Catalog
{
    public enum SubmissionStatus
    {
        [Display(Name = "En attente")]
        Pending, // Produit soumis mais pas encore validé
        Approved, // Produit validé
        Rejected // Produit rejeté
    }
}
