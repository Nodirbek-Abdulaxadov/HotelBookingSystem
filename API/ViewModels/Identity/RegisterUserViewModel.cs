using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Identity
{
    public class RegisterUserViewModel
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public string UserRole { get; set; }
    }
}
