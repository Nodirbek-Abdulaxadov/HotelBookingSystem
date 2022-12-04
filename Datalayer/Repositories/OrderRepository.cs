using Datalayer.Context;
using Entities;
using Datalayer.Interfaces;
namespace Datalayer.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderInterface
    {
        public OrderRepository(HotelDbContext dbContext) : base(dbContext)
        {
        }
    }
}
