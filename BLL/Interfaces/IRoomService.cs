using BLL.DTOs.Rooms;
using Entities;

namespace BLL.Interfaces
{
    internal interface IRoomService
    {
        Task<IEnumerable<Room>> GetEmptyRoomsAsync();
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> AddAsync(AddRoomDto room);
        Task<Room> UpdateAsync(UpdateRoomDto room);
        Task RemoveAsync(int roomId);
    }
}
