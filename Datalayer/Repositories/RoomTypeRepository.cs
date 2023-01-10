using Datalayer.Context;
using Entities;
using Datalayer.Interfaces;

namespace Datalayer.Repositories
{
    public class RoomTypeRepository : Repository<RoomType>, IRoomTypeInterface
    {
        public RoomTypeRepository(HotelDbContext dbContext) : base(dbContext)
        {
        }
    }
}
