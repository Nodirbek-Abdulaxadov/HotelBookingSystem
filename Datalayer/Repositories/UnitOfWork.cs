using Datalayer.Context;
using Datalayer.Interfaces;

namespace Datalayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HotelDbContext _dbContext;

        public UnitOfWork(IRoomInterface rooms,
                          IOrderInterface orders,
                          IServiceInterface services,
                          IReceiptInterface receipts,
                          HotelDbContext dbContext)
        {
            Rooms = rooms;
            Orders = orders;
            Services = services;
            Receipts = receipts;
            _dbContext = dbContext;
            _dbContext = dbContext;
        }
        public IRoomInterface Rooms { get; }

        public IOrderInterface Orders { get; }

        public IServiceInterface Services { get; }

        public IReceiptInterface Receipts { get; }

        public void Dispose()
            => GC.SuppressFinalize(this);

        public async Task SaveAsync()
            => await _dbContext.SaveChangesAsync();
    }
}
