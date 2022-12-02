using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.UserViewModels
{
    public class LoginUserViewModel
    {
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
