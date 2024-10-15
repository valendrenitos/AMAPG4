using System.ComponentModel.DataAnnotations;

namespace AMAPG4.Models.User
{
    public class CE
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [MinLength(2), MaxLength(50)]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "Le nombre d'employé est obligatoire.")]
        [MinLength(1), MaxLength(3)]
        public int NumberOfEmployees { get; set; }
        public bool IsContributionPaid { get; set; }
        public virtual UserAccount Account { get; set; }
    }
}
