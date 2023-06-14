using BLL.DTOs.Orders;
using BLL.Interfaces;
using Datalayer.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IOrderService orderService,
                                UserManager<User> userManager,
                                IRoomTypeService typeService,
                                IRoomService roomService,
                                IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            _userManager = userManager;
            _typeService = typeService;
            _roomService = roomService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(OrdersViewModel viewModel)
        {
            var listOfOrders = await _orderService.GetAllOrdersAsync();
            List<ViewOrderDto> orders = new();
            foreach (var order in listOfOrders.Where(o => o.OrderStatus != OrderStatus.Unknown))
            {
                var guest = _userManager.Users.FirstOrDefault(i => i.Id == order.GuestId);
                var type = await _typeService.GetByIdAsync(order.RoomTypeId);

                orders.Add(new ViewOrderDto
                {
                    Id = order.Id,
                    FullName = $"{guest.FirstName} {guest.LastName}",
                    StartDate = order.StartDate,
                    EndDate = order.EndDate,
                    TotalPrice = order.TotalPrice,
                    RoomType = type,
                    Status = order.OrderStatus.ToString()
                });
            }

            if (!string.IsNullOrEmpty(viewModel.SearchText))
            {
                orders = orders.Where(i => i.FullName.ToLower().Contains(viewModel.SearchText.ToLower())).ToList();
            }

            viewModel.Orders = orders;
            
            return View(viewModel);
        }

        public async Task<IActionResult> Active(OrdersViewModel viewModel)
        {
            var listOfOrders = await _orderService.GetAllOrdersAsync();
            List<ViewOrderDto> orders = new();
            foreach (var order in listOfOrders.Where(o => o.OrderStatus == OrderStatus.Active))
            {
                var guest = _userManager.Users.FirstOrDefault(i => i.Id == order.GuestId);
                var type = await _typeService.GetByIdAsync(order.RoomTypeId);

                orders.Add(new ViewOrderDto
                {
                    Id = order.Id,
                    FullName = $"{guest.FirstName} {guest.LastName}",
                    StartDate = order.StartDate,
                    EndDate = order.EndDate,
                    TotalPrice = order.TotalPrice,
                    RoomType = type,
                    Status = order.OrderStatus.ToString()
                });
            }

            if (!string.IsNullOrEmpty(viewModel.SearchText))
            {
                orders = orders.Where(i => i.FullName.ToLower().Contains(viewModel.SearchText.ToLower())).ToList();
            }

            viewModel.Orders = orders;

            return View(viewModel);
        }

        public async Task<IActionResult> Activate(int orderId)
        {
            var order = await _orderService.GetByIdAsync(orderId);
            order.OrderStatus = OrderStatus.Active;
            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.SaveAsync();

            return RedirectToAction("index", "orders");
        }

        public async Task<IActionResult> Details(int orderId)
        {
            return View();
        }

        public async Task<IActionResult> Filter(string type)
        {
            var listOfOrders = await _orderService.GetAllOrdersAsync();
            List<ViewOrderDto> orders = new();
            foreach (var order in listOfOrders.Where(o => o.OrderStatus != OrderStatus.Unknown))
            {
                var guest = _userManager.Users.FirstOrDefault(i => i.Id == order.GuestId);
                var roomType = await _typeService.GetByIdAsync(order.RoomTypeId);

                orders.Add(new ViewOrderDto
                {
                    Id = order.Id,
                    FullName = $"{guest.FirstName} {guest.LastName}",
                    StartDate = order.StartDate,
                    EndDate = order.EndDate,
                    TotalPrice = order.TotalPrice,
                    RoomType = roomType,
                    Status = order.OrderStatus.ToString()
                });
            }
            OrdersViewModel viewModel = new();
            viewModel.Orders = orders;
            viewModel.Type = type;
            switch (type)
            {
                case "all":
                    {
                        return View(viewModel);
                    }
                case "waiting":
                    {
                        viewModel.Orders = viewModel.Orders.Where(o => o.Status == "Waiting");
                        return View(viewModel);
                    }
                case "confirmed":
                    {
                        viewModel.Orders = viewModel.Orders.Where(o => o.Status == "Confirmed");
                        return View(viewModel);
                    }
                case "declined":
                    {
                        viewModel.Orders = viewModel.Orders.Where(o => o.Status == "Declined");
                        return View(viewModel);
                    }
                case "completed":
                    {
                        viewModel.Orders = viewModel.Orders.Where(o => o.Status == "Completed");
                        return View(viewModel);
                    }
            }

            return View(viewModel);
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
            return RedirectToAction("pending");
        }

        [HttpGet]
        public async Task<IActionResult> Accept(int orderId)
        {
            var model = await GetViewModel(orderId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Accept(AcceptOrderDto vm)
        {
            List<Room> selectedRooms = new();
            var rooms = await _roomService.GetAllRoomsAsync();
            selectedRooms = vm.RoomChecks.Where(i => i.IsChecked == true)
                                         .Select(i => rooms.FirstOrDefault(r => r.Id == i.Id))
                                         .ToList();
            var order = await _orderService.GetByIdAsync(vm.OrderId);

            var peopleCount = order.NumberOfAdults + order.NumberOfChildren;
            var type = (await _typeService.GetAllAsync())
                                          .FirstOrDefault(i => i.Id == order.RoomTypeId);
            var bedCount = selectedRooms.Count * type.Capacity;
            if (bedCount < peopleCount)
            {
                var model = await GetViewModel(vm.OrderId, "There is no enough space for all guests!");
                return View(model);
            }
            else
            {
                await _orderService.AcceptOrderAsync(order, selectedRooms);
                return RedirectToAction("index");
            }
        }

        private async Task<AcceptOrderDto> GetViewModel(int orderId)
        {
            var order = await _orderService.GetByIdAsync(orderId);
            var rooms = await _roomService.GetAllRoomsAsync();
            List<RoomSelect> roomSelects = new List<RoomSelect>();
            foreach (var room in rooms.Where(i => i.RoomType.Id == order.RoomTypeId))
            {
                roomSelects.Add(new RoomSelect()
                {
                    Room = room,
                    IsChecked = false,
                    Id = room.Id
                });
            }
            AcceptOrderDto model = new()
            {
                Order = order,
                OrderId = order.Id,
                RoomChecks = roomSelects,
                User = _userManager.Users.FirstOrDefault(i => i.Id == order.GuestId)
            };

            return model;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        private async Task<AcceptOrderDto> GetViewModel(int orderId, string errorMessage)
        {
            var model = await GetViewModel(orderId);
            model.HasError = true;
            model.ErrorMessage = errorMessage;
            return model;
        }
    }
}
