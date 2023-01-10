using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Identity
{
    public class TokenRequstViewModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
