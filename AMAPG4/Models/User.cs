using System.ComponentModel.DataAnnotations;

namespace AMAPG4.Models
{
    public class UserAccount
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MinLength(8)]
        public string Password { get; set; }
        [MinLength(2), MaxLength(40)]
        public string Name { get; set; }
        [MinLength(2), MaxLength(120)]
        public string Address { get; set; }
        [MinLength(10), MaxLength(10)]
        public int Phone { get; set; }
        
    }
}
