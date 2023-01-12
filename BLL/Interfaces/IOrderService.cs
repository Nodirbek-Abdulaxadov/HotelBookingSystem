using BLL.DTOs.Orders;
using Entities;

namespace BLL.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> CreateOrderAsync(AddOrderDto order);
        Task<Order> ConfirmOrderAsync(int orderId);
        Task<Order> DeclineOrderAsync(int orderId);
        Task<Order> GetByIdAsync(int id);
    }
}
