using API.Areas.Admin.ViewModels;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> userManager;

        public AuthController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(viewModel.Email);
                if (user == null)
                {
                    ModelState.AddModelError(nameof(viewModel.Email), $"This email coudn't found!");
                }

                bool passwordValid = await userManager.CheckPasswordAsync(user, viewModel.Password);

                if (passwordValid)
                {
                    return RedirectToAction("index", "home");
                }

                return View();
            }
            return View();
        }
    }
}
