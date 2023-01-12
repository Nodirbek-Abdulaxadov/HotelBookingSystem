namespace Entities
{
    public enum OrderStatus
    {
        Unknown,
        Waiting,
        Confirmed,
        Active,
        Completed,
        Declined
    }

    public enum RoomStatus
    {
        Unknown,
        Empty,
        Busy,
        Ordered
    }
}
