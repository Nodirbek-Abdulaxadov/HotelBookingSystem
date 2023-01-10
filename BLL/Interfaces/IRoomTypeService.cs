using BLL.DTOs.RoomTypes;
using Entities;

namespace BLL.Interfaces
{
    public interface IRoomTypeService
    {
        //Task<bool> CheckAsync(string type, decimal price);
        //Task<IEnumerable<ViewRoomTypeDto>> GetEmptyRoomTypesAsync(string language);
        //Task<ViewRoomTypeDto> GetByNumberAsync(int RoomTypeNumber, string language);
        //Task<RoomType> GetByNumberAsync(int RoomTypeNumber);
        //Task<IEnumerable<RoomType>> GetEmptyRoomTypesAsync();

        Task<IEnumerable<ViewRoomTypeDto>> GetAllAsync(string language);
        Task<ViewRoomTypeDto> GetByIdAsync(int RoomTypeId, string language);

        Task<IEnumerable<RoomType>> GetAllAsync();
        Task<RoomType> GetByIdAsync(int RoomTypeId);
        Task<RoomType> AddAsync(AddRoomTypeDto RoomType);
        Task<RoomType> UpdateAsync(UpdateRoomTypeDto RoomType);
        Task RemoveAsync(int RoomTypeId);
    }
}
