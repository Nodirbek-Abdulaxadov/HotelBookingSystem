namespace Datalayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRoomInterface Rooms { get; }
        IOrderInterface Orders { get; }
        IServiceInterface Services { get; }
        IReceiptInterface Receipts { get; }
        Task SaveAsync();
    }
}
