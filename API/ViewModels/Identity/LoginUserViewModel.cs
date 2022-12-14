using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Identity
{
    public class LoginUserViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
