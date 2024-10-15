using System.ComponentModel.DataAnnotations;

namespace AMAPG4.Models.User
{
    public class UserAccount
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [MinLength(2), MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [MinLength(2), MaxLength(120)]
        public string Address { get; set; }
        [Required]
        [MinLength(10), MaxLength(10)]
        public string Phone { get; set; }

    }
}
