using API.ViewModels.UserViewModels;

namespace API.Interfaces
{
    public interface IUserService
    {
        Task<(bool, string)> CreateUserAsync(RegisterUserViewModel viewModel);
        Task<(bool, string)> LoginUserAsync(LoginUserViewModel viewModel);
    }
}
