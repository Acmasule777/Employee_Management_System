using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="First Name is Required.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage ="Last Name is Required.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage ="Email is Required. ")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage ="Password is Required. ")]
        [StringLength(40, MinimumLength =8, ErrorMessage ="The {0} must be at {2} and at max {1} characters long. ")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required. ")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Confirm Password does not match. ")]
        [Display(Name ="Confirm Password")]
        public string? ConfirmPassword { get; set; }
    }
}
