using Datalayer.Context;
using Entities;
using Datalayer.Interfaces;

namespace Datalayer.Repositories
{
    public class ServiceRepository : Repository<Service>, IServiceInterface
    {
        public ServiceRepository(HotelDbContext dbContext) : base(dbContext)
        {
        }
    }
}
