using System.ComponentModel.DataAnnotations;

namespace CourseBookingSystem.PL.ViewModels
{
    public class ClientFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The FullName field is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "The PhoneNumber field is required.")]
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{11}$", ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Gender field is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "The ServiceId field is required.")]
        public int ServiceId { get; set; }

        public string SomeServiceDetails { get; set; }
    }
}
