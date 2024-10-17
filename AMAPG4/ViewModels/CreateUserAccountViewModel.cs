using AMAPG4.Models.User;
using System.ComponentModel.DataAnnotations;

namespace AMAPG4.ViewModels
{
    public class CreateUserAccountViewModel
    {
        public UserAccount UserAccount { get; set; }
       
        public CE CE { get; set; }
        public Individual Individual { get; set; }
        public Producer Producer { get; set; }

        [Required(ErrorMessage = "Veuillez confirmer votre mot de passe")]
      // [DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas")]
        public string ConfirmPassword { get; set; }

     

    }
}
