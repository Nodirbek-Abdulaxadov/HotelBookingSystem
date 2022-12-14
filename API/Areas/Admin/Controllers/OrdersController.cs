using BLL.DTOs.Orders;
using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Areas.Admin.Controllers
{
    [Area("admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<User> _userManager;
        private readonly IRoomTypeService _typeService;
        private readonly IRoomService _roomService;

        public OrdersController(IOrderService orderService,
                                UserManager<User> userManager,
                                IRoomTypeService typeService,
                                IRoomService roomService)
        {
            _orderService = orderService;
            _userManager = userManager;
            _typeService = typeService;
            _roomService = roomService;
        }

        public async Task<IActionResult> Pending()
        {
            var listOfOrders = await _orderService.GetAllOrdersAsync();
            listOfOrders = listOfOrders.Where(i => i.OrderStatus == OrderStatus.Waiting)
                                       .OrderByDescending(o => o.BookedDate);
            List<PendingOrder> pendingOrders = new List<PendingOrder>();
            foreach (var order in listOfOrders)
            {
                var guest = _userManager.Users.FirstOrDefault(i => i.Id == order.GuestId);
                var type = await _typeService.GetByIdAsync(order.RoomTypeId);

                pendingOrders.Add(new PendingOrder
                {
                    Id = order.Id,
                    FullName = $"{guest.FirstName} {guest.LastName}",
                    StartDate = order.StartDate,
                    EndDate = order.EndDate,
                    TotalPrice = order.TotalPrice,
                    RoomType = type
                });
            }

            return View(pendingOrders);
        }

        public async Task<IActionResult> Decline(int orderId)
        {
            var order = await _orderService.GetByIdAsync(orderId);
            await _orderService.DeclineOrderAsync(orderId);
            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> Accept(int orderId)
        {
            var order = await _orderService.GetByIdAsync(orderId);
            var rooms = await _roomService.GetAllRoomsAsync();
            List<RoomSelect> roomSelects = new List<RoomSelect>();
            foreach (var room in rooms.Where(i => i.RoomType.Id == order.RoomTypeId))
            {
                roomSelects.Add(new RoomSelect()
                {
                    Room = room,
                    IsChecked = false
                });
            }
            AcceptOrderDto model = new()
            {
                Order = order,
                RoomChecks = roomSelects,
                User = _userManager.Users.FirstOrDefault(i => i.Id == order.GuestId)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Accept(AcceptOrderDto vm)
        {
            
            return View();
        }
    }
}
