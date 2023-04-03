using Datalayer.Context;
using Datalayer.Interfaces;

namespace Datalayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HotelDbContext _dbContext;

        public UnitOfWork(IRoomTypeInterface roomTypes,
                          IOrderInterface orders,
                          IServiceInterface services,
                          IReceiptInterface receipts,
                          IRoomInterface roomInterface,
                          HotelDbContext dbContext)
        {
            RoomTypes = roomTypes;
            Orders = orders;
            Services = services;
            Receipts = receipts;
            Rooms = roomInterface;
            _dbContext = dbContext;
        }

        public IRoomTypeInterface RoomTypes { get; }
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
