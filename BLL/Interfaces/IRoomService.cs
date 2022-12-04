using BLL.DTOs.Rooms;
using Entities;

namespace BLL.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetEmptyRoomsAsync();
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room?> GetByIdAsync(int roomId);
        Task<Room?> GetByNumberAsync(int roomNumber);
        Task<Room> AddAsync(AddRoomDto room);
        Task<Room> UpdateAsync(UpdateRoomDto room);
        Task RemoveAsync(int roomId);
    }
}
