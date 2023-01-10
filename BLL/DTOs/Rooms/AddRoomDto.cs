using Entities;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs.Rooms
{
    public class AddRoomDto
    {
        [Required]
        public int Number { get; set; }
        [Required]
        public RoomStatus Status { get; set; }

        public int RoomTypeId { get; set; }

        public List<RoomType> RoomTypes { get; set; } = new List<RoomType>();

        public static explicit operator Room(AddRoomDto v)
        {
            return new Room()
            {
                Number = v.Number,
                Status = v.Status
            };
        }
    }
}
