using Entities;

namespace BLL.DTOs.Orders
{
    public class AcceptOrderDto
    {
        public AcceptOrderDto()
        {
            RoomChecks = new List<RoomSelect>();
        }

        public Order Order { get; set; }
        public User User { get; set; }
        public List<RoomSelect> RoomChecks { get; set; }
    }

    public class RoomSelect
    {
        public Room Room { get; set; }
        public bool IsChecked { get; set; }
    }
}
