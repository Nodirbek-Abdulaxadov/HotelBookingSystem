using Entities;

namespace BLL.DTOs.Rooms
{
    public class ViewRoomDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Number { get; set; }
        public RoomStatus Status { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImagePath { get; set; }
    }
}
