using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Identity
{
    public class RegisterUserViewModel
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        [Required]
        public string? UserRole { get; set; }
    }
}
