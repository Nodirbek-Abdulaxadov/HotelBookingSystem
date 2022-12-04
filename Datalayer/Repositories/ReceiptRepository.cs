using Datalayer.Context;
using Entities;
using Datalayer.Interfaces;

namespace Datalayer.Repositories
{
    public class ReceiptRepository : Repository<Receipt>, IReceiptInterface
    {
        public ReceiptRepository(HotelDbContext dbContext) : base(dbContext)
        {
        }
    }
}
