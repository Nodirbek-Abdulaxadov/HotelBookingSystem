using BLL.DTOs.Services;
using Entities;

namespace BLL.Interfaces
{
    public interface IService
    {
        Task<Service> AddAsync(AddServiceDto service);
        Task<IEnumerable<Service>> GetAllAsync();
        Task<IEnumerable<Service>> GetAllReceiptServicesAsync(int receiptId);
        Task<Service?> GetByIdAsync(int serviceId);
        Task<Service> UpdateAsync(Service service);
        Task RemoveAsync(int serviceId);
    }
}
