using BLL.DTOs.Rooms;
using Entities;

namespace BLL.Interfaces
{
    public interface IRoomService
    {
        Task<bool> CheckAsync(string type, decimal price);
        Task<IEnumerable<ViewRoomDto>> GetEmptyRoomsAsync(string language);
        Task<IEnumerable<ViewRoomDto>> GetAllAsync(string language);
        Task<ViewRoomDto?> GetByIdAsync(int roomId, string language);
        Task<ViewRoomDto?> GetByNumberAsync(int roomNumber, string language);

        Task<IEnumerable<Room>> GetEmptyRoomsAsync();
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room?> GetByIdAsync(int roomId);
        Task<Room?> GetByNumberAsync(int roomNumber);
        Task<Room> AddAsync(AddRoomDto room);
        Task<Room> UpdateAsync(UpdateRoomDto room);
        Task RemoveAsync(int roomId);
    }
}
