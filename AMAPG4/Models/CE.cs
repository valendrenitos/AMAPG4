using System.ComponentModel.DataAnnotations;

namespace AMAPG4.Models
{
    public class CE
    {
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [MinLength(2), MaxLength(50)]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "Le nombre d'employé est obligatoire.")]
        [MinLength(1), MaxLength(3)]
        public int NumberOfEmployees { get; set; }
        public bool IsContributionPaid { get; set; }
        UserAccount Account { get; set; }
    }
}
