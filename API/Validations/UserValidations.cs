using Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Validations
{
    public static class UserValidations
    {
        public static User? FindByPhoneNumber(this UserManager<User> userManager, string? phoneNumber)
            => userManager.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
    }
}
