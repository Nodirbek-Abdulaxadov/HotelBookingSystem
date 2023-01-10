using Datalayer.Context;
using Datalayer.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Datalayer.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomInterface
    {
        private readonly HotelDbContext _dbContext;

        public RoomRepository(HotelDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Room>> GetAllWithRoomTypesAsync()
        {
            var list = await _dbContext.Rooms?.Include(i => i.RoomType)
                                              .ToListAsync();
            return list;
        }

        public async Task<Room> GetByIdWithRoomTypesAsync(int id)
        {
            var model = await _dbContext.Rooms?.Include(i => i.RoomType)
                                               .FirstOrDefaultAsync();
            return model;
        }
    }
}
