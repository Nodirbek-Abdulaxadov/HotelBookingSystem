using API.Identity;
using API.Interfaces;
using API.ViewModels.Identity;
using BLL.DTOs.Guests;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PasswordGenerator;

namespace API.Areas.Admin.Controllers
{
    [Area("admin")]
    public class GuestsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public GuestsController(UserManager<User> userManager,
                                IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<IActionResult> Index(GuestsViewModel viewModel)
        {
            var guests = new List<User>();
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                var UserIsGuest = await _userManager.IsInRoleAsync(user, "guest");
                if (UserIsGuest)
                {
                    guests.Add(user);
                }
            }

            viewModel.Guests = guests;

            if (!string.IsNullOrEmpty(viewModel.SearchText))
            {
                var text = viewModel.SearchText;
                viewModel.Guests = guests.Where(i => i.FirstName.ToLower().Contains(text.ToLower()) ||
                                                     i.LastName.ToLower().Contains(text.ToLower())).ToList();
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RegisterUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var pwd = new Password(32);
                var password = pwd.Next();
                viewModel.Password = password;
                viewModel.ConfirmPassword = password;
                viewModel.UserRole = UserRoles.Guest;

                var res = await _userService.CreateUserAsync(viewModel);

                if (res.Item1 == true)
                {
                    return RedirectToAction("index");
                }
                else
                {
                    ModelState.AddModelError(nameof(viewModel.FirstName), res.Item2);
                    return View();
                }
            }

            return View();
        }
    }
}
