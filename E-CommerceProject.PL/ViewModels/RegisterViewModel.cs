using System.ComponentModel.DataAnnotations;

namespace CourseBookingSystem.PL.ViewModels
{     
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "The UserName field is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The UserEmail field is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "The UserPhone field is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string UserPhone { get; set; }

        [Required(ErrorMessage = "The UserPassword field is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string UserPassword { get; set; }
        [Compare("UserPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string UserConfirmPassword { get; set; }
    }
}
