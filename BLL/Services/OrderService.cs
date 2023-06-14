using BLL.DTOs.Orders;
using BLL.Interfaces;
using Datalayer.Interfaces;
using Entities;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AcceptOrderAsync(Order order, List<Room> rooms)
        {
            order.Rooms = rooms;
            order.OrderStatus = OrderStatus.Confirmed;
            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Order> ConfirmOrderAsync(int orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            order.OrderStatus = OrderStatus.Confirmed;
            order.ConfirmedDate = DateTime.UtcNow.ToString();
            order = await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.SaveAsync();

            return order;
        }

        public async Task<Order> CreateOrderAsync(AddOrderDto order)
        {
            var model = (Order)order;
            model = await _unitOfWork.Orders.AddAsync(model);
            await _unitOfWork.SaveAsync();

            return model;
        }

        public async Task<Order> DeclineOrderAsync(int orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            order.OrderStatus = OrderStatus.Declined;
            order.ConfirmedDate = DateTime.UtcNow.ToString();
            order = await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.SaveAsync();

            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();
            return orders.OrderByDescending(o => o.Id).ToList();
        }

        public async Task<Order> GetByIdAsync(int id)
            => await _unitOfWork.Orders.GetByIdAsync(id);

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            var list = await _unitOfWork.Orders.GetAllAsync();
            list = list.Where(o => o.OrderStatus == status);

            return list;
        }
    }
}
