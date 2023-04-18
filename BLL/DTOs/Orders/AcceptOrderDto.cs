using Entities;

namespace BLL.DTOs.Orders
{
    public class AcceptOrderDto
    {
        public AcceptOrderDto()
        {
            RoomChecks = new List<RoomSelect>();
        }

        public bool HasError { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        public int OrderId { get; set; } = 0;

        public Order Order { get; set; }
        public User User { get; set; }
        public List<RoomSelect> RoomChecks { get; set; }
    }

    public class RoomSelect
    {
        public int Id { get; set; }
        public Room Room { get; set; }
        public bool IsChecked { get; set; }
    }
}
