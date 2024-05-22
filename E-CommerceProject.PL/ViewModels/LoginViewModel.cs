using System.ComponentModel.DataAnnotations;

namespace CourseBookingSystem.PL.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The UserName field is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The UserPassword field is required.")]
        public string UserPassword { get; set; }
    }
}
