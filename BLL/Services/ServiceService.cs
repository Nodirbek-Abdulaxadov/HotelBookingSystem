using BLL.DTOs.Services;
using BLL.Interfaces;
using Datalayer.Interfaces;
using Entities;

namespace BLL.Services
{
    public class ServiceService : IService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Service> AddAsync(AddServiceDto service)
        {
            var model = await _unitOfWork.Services.AddAsync((Service)service);
            await _unitOfWork.SaveAsync();

            return model;
        }

        public Task<IEnumerable<Service>> GetAllAsync()
            => _unitOfWork.Services.GetAllAsync();

        public async Task<IEnumerable<Service>> GetAllReceiptServicesAsync(int receiptId)
        {
            var list = await _unitOfWork.Services.GetAllAsync();
            return list.Where(i => i.ReceiptId == receiptId);
        }

        public async Task<Service?> GetByIdAsync(int serviceId)
            => await _unitOfWork.Services.GetByIdAsync(serviceId);

        public async Task RemoveAsync(int serviceId)
        {
            var model = await _unitOfWork.Services.GetByIdAsync(serviceId);
            await _unitOfWork.Services.RemoveAsync(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Service> UpdateAsync(Service service)
        {
            var model = await _unitOfWork.Services.UpdateAsync(service);
            await _unitOfWork.SaveAsync();

            return model;
        }
    }
}
