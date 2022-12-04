using Datalayer.Context;
using Entities;
using Datalayer.Interfaces;

namespace Datalayer.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomInterface
    {
        public RoomRepository(HotelDbContext dbContext) : base(dbContext)
        {
        }
    }
}
