using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API.Areas.Admin.ViewModels;

public class LoginViewModel
{
    [EmailAddress]
    [Required]
    public string Email { get; set; } = string.Empty;
    [PasswordPropertyText]
    [Required]
    public string Password { get; set; } = string.Empty;
}