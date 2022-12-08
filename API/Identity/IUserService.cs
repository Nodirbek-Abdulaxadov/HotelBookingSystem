using API.ViewModels.Identity;
using Entities;
using System.Security.Claims;

namespace API.Interfaces
{
    public interface IUserService
    {
        Task<(bool, string)> CreateUserAsync(RegisterUserViewModel viewModel);
        Task<(bool, string)> LoginUserAsync(LoginUserViewModel viewModel);
        Task<AuthResultViewModel> VerifyAndGenerateTokenAsync(TokenRequstViewModel viewModel);
    }
}
