using Entities;

namespace Datalayer.Interfaces
{
    public interface IRoomInterface : IRepository<Room>
    {
        Task<IEnumerable<Room>> GetAllWithRoomTypesAsync();
        Task<Room> GetByIdWithRoomTypesAsync(int id);
    }
}
