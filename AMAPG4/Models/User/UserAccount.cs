using System.ComponentModel.DataAnnotations;

namespace AMAPG4.Models.User
{
    public class UserAccount
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Veuillez renseigner votre email")]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Veuillez renseigner votre mot de passe")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères, une lettre majuscule, une lettre minuscule, un chiffre et un caractère spécial.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Veuillez renseigner votre nom")]
        [MinLength(2), MaxLength(40)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Veuillez renseigner votre adresse")]
        [MinLength(2), MaxLength(120)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Veuillez renseigner votre téléphone")]
        [MinLength(10), MaxLength(10)]
        public string Phone { get; set; }
        public Role Role { get; set; } = Role.Utilisateur;

    }
}
