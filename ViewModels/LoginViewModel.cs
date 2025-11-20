using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please Enter your Valid Email Address.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your Valid Password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]

        public bool RememberMe { get; set; }
    }
}
