using BLL.DTOs.Orders;
using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<User> _userManager;
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;

        public OrdersController(IOrderService orderService,
                               UserManager<User> userManager,
                               IRoomTypeService roomTypeService,
                               IRoomService roomService)
        {
            _orderService = orderService;
            _userManager = userManager;
            _roomService = roomService;
            _roomTypeService = roomTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            return Ok(orders);
        }

        [HttpGet("waiting")]
        public async Task<IActionResult> GetWaitingOrders()
        {
            var orders = await _orderService.GetOrdersByStatusAsync(OrderStatus.Waiting);

            return Ok(orders);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveOrders()
        {
            var orders = await _orderService.GetOrdersByStatusAsync(OrderStatus.Active);

            return Ok(orders);
        }

        [HttpGet("confirmed")]
        public async Task<IActionResult> GetConfirmedOrders()
        {
            var orders = await _orderService.GetOrdersByStatusAsync(OrderStatus.Confirmed);

            return Ok(orders);
        }

        [HttpGet("completed")]
        public async Task<IActionResult> GetCompletedOrders()
        {
            var orders = await _orderService.GetOrdersByStatusAsync(OrderStatus.Completed);

            return Ok(orders);
        }

        [HttpGet("check")]
        public async Task<IActionResult> Check(string email, int roomTypeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("User not found!");
            }

            var room = await _roomTypeService.GetByIdAsync(roomTypeId);
            if (room == null)
            {
                return BadRequest("Room not found!");
            }

            var res = await _roomService.CheckAsync(roomTypeId);

            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompletedOrders(int id)
        {
            var order = await _orderService.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost("confirm/{orderId}")]
        public async Task<IActionResult> Confirm(int orderId)
        {
            var order = await _orderService.ConfirmOrderAsync(orderId);

            return Ok(order);
        }

        [HttpPost("create/{email}")]
        public async Task<IActionResult> Create(string email, [FromBody] AddOrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("User not found!");
            }

            orderDto.GuestId = user.Id;

            var model = await _orderService.CreateOrderAsync(orderDto);

            return Ok(model);
        }
    }
}
