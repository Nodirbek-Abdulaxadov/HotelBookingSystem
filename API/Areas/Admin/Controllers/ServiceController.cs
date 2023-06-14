using API.Areas.Admin.ViewModels;
using Datalayer.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Add(int orderId)
            => View(new AddServiceViewModel() { OrderId = orderId});

        [HttpPost]
        public async Task<IActionResult> Add(AddServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Services.AddAsync(new Service()
                {
                    Name = model.Name,
                    Price = model.Price,
                    ReceiptId = model.OrderId
                });
                await _unitOfWork.SaveAsync();
                return RedirectToAction("active", "orders");
            }
            return View(model);
        }
    }
}
