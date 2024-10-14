using System.ComponentModel.DataAnnotations;

namespace AMAPG4.Models
{
    public class Producer
    {
        [Required(ErrorMessage = "Le numéro SIRET est obligatoire.")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "Le numéro SIRET doit comporter 14 chiffres.")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "Le numéro SIRET doit contenir uniquement 14 chiffres.")] 
        public string Siret { get; set; }
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [MinLength(2), MaxLength(50)]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Le RIB est obligatoire.")]
        [StringLength(23, MinimumLength = 23, ErrorMessage = "Le RIB doit comporter 23 caractères.")]
        [RegularExpression(@"^[A-Za-z0-9]{23}$", ErrorMessage = "Le RIB doit contenir exactement 23 caractères alphanumériques.")]
        public string RIB { get; set; }
        UserAccount Account { get; set; }
    }
}
