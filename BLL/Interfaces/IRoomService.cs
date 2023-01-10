using BLL.DTOs.Rooms;
using Entities;

namespace BLL.Interfaces
{
    public interface IRoomService
    {
        //Task<bool> CheckAsync(string type, decimal price);
        //Task<IEnumerable<ViewRoomDto>> GetEmptyRoomsAsync(string language);
        //Task<ViewRoomDto> GetByNumberAsync(int RoomNumber, string language);
        //Task<Room> GetByNumberAsync(int RoomNumber);
        //Task<IEnumerable<Room>> GetEmptyRoomsAsync();

        Task<IEnumerable<ViewRoomDto>> GetAllAsync();
        Task<Room> GetByIdAsync(int RoomId);
        Task<ViewRoomDto> GetByIdForViewAsync(int RoomId);
        Task AddAsync(AddRoomDto Room);
        Task UpdateAsync(UpdateRoomDto room);
        Task RemoveAsync(int RoomId);
    }
}
