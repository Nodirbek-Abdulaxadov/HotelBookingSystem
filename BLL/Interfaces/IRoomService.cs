using BLL.DTOs.Rooms;
using Entities;

namespace BLL.Interfaces
{
    public interface IRoomService
    {
        Task<bool> CheckAsync(int roomTypeId);
        //Task<IEnumerable<ViewRoomDto>> GetEmptyRoomsAsync(string language);
        //Task<ViewRoomDto> GetByNumberAsync(int RoomNumber, string language);
        //Task<Room> GetByNumberAsync(int RoomNumber);
        //Task<IEnumerable<Room>> GetEmptyRoomsAsync();

        Task<IEnumerable<ViewRoomDto>> GetAllAsync();
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<Room> GetByIdAsync(int RoomId);
        Task<ViewRoomDto> GetByIdForViewAsync(int RoomId);
        Task AddAsync(AddRoomDto Room);
        Task UpdateAsync(UpdateRoomDto room);
        Task RemoveAsync(int RoomId);
        Task<bool> CheckAsync(string startDate, string endDate, int guestsCount);
    }
}
