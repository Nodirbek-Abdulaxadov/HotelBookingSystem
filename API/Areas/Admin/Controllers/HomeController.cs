using API.Areas.Admin.ViewModels;
using API.Identity;
using Datalayer.Interfaces;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public HomeController(IUnitOfWork unitOfWork,
                              UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel();
            var orders = await _unitOfWork.Orders.GetAllAsync();
            viewModel.OrdersCount = orders.Count();
            viewModel.PendingOrdersCount = orders
                .Where(o => o.OrderStatus == OrderStatus.Waiting)
                .Count();
            viewModel.GuestsCount = 0;
            foreach (var user in _userManager.Users.ToList())
            {
                if (await _userManager.IsInRoleAsync(user, UserRoles.Guest))
                {
                    viewModel.GuestsCount++;
                }
            }

            viewModel.TotalBalance = orders.Where(o => o.OrderStatus == OrderStatus.Completed)
                .Sum(i => i.TotalPrice);

            return View(viewModel);
        }
    }
}
