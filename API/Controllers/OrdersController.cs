using BLL.DTOs.Orders;
using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
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

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AddOrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = await _orderService.CreateOrderAsync(orderDto);

            return Ok(model);
        }
    }
}
