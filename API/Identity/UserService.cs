using API.Identity;
using API.ViewModels.Identity;
using Datalayer.Context;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace API.Interfaces
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly HotelDbContext _dbContext;
        private readonly TokenValidationParameters _validationParameters;

        public UserService(UserManager<User> userManager,
                           RoleManager<IdentityRole> roleManager,
                           IConfiguration configuration,
                           HotelDbContext dbContext,
                           TokenValidationParameters validationParameters)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _dbContext = dbContext;
            _validationParameters = validationParameters;
        }
        public async Task<(bool, string)> CreateUserAsync(RegisterUserViewModel viewModel)
        {
            var userExist = await _userManager.FindByEmailAsync(viewModel.Email);
            if (userExist != null)
            {
                return (false, "This phone number is already exist!");
            }

            User user = new()
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
                UserName = viewModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
            {
                switch (viewModel.UserRole)
                {
                    case UserRoles.SuperAdmin:
                        await _userManager.AddToRoleAsync(user, UserRoles.SuperAdmin); break;
                    case UserRoles.Admin:
                        await _userManager.AddToRoleAsync(user, UserRoles.Admin); break;
                    case UserRoles.Guest:
                        await _userManager.AddToRoleAsync(user, UserRoles.Guest); break;
                }

                return (true, "User Created!");
            }

            return (false, result.Errors.First().Description);
        }

        public async Task<(bool, string)> LoginUserAsync(LoginUserViewModel viewModel)
        {
            var userExist = await _userManager.FindByEmailAsync(viewModel.Email);
            if (userExist != null && await _userManager.CheckPasswordAsync(userExist, viewModel.Password))
            {
                return (true, JsonConvert.SerializeObject(await GeneraTokenAsync(userExist, null)));
            }

            return (false, "Login failed! Incorrect email or password!");
        }

        private async Task<AuthResultViewModel> GeneraTokenAsync(User user, RefreshToken? refresh)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.PhoneNumber, user.PhoneNumber),
                new Claim(JwtRegisteredClaimNames.Sub, user.PhoneNumber),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:securityKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audence"],
                expires: DateTime.UtcNow.AddSeconds(10),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256));

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            if (refresh != null)
            {
                var rToken = new AuthResultViewModel()
                {
                    FullName = user.FirstName + " " + user.LastName,
                    Email = user.Email,
                    Token = jwtToken,
                    RefreshToken = refresh.Token,
                    ExpiresAt = token.ValidTo,
                };
                return rToken;
            }

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsRevoked = false,
                UserId = Guid.Parse(user.Id),
                DateAdded = DateTime.UtcNow.ToString(),
                DataExpire = DateTime.UtcNow.AddMonths(6).ToString(),
                Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString()
            };

            await _dbContext.RefreshTokens.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();

            var response = new AuthResultViewModel()
            {
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email,
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = token.ValidTo,
            };

            return response;
        }

        public async Task<AuthResultViewModel> VerifyAndGenerateTokenAsync(TokenRequstViewModel viewModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var storedToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(i => i.Token == viewModel.Token);

            var user = await _userManager.FindByIdAsync(storedToken.UserId.ToString());

            try
            {
                var tokenCheckResult = tokenHandler.ValidateToken(viewModel.Token,
                                                                  _validationParameters,
                                                                  out var validatedToken);
                return await GeneraTokenAsync(user, storedToken);
            }
            catch (SecurityTokenExpiredException)
            {
                if (DateTime.Parse(storedToken.DataExpire) >= DateTime.UtcNow)
                {
                    return await GeneraTokenAsync(user, storedToken);
                }
                else
                {
                    return await GeneraTokenAsync(user, null);
                }
            }
        }
    }
}
