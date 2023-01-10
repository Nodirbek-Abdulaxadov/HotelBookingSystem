using Entities;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs.Rooms
{
    public class UpdateRoomDto
    {
        public int Id { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public RoomStatus Status { get; set; }

        public int RoomTypeId { get; set; }

        public List<RoomType> RoomTypes { get; set; } = new List<RoomType>();

        public static explicit operator Room(UpdateRoomDto v)
        {
            return new Room()
            {
                Id = v.Id,
                Number = v.Number,
                Status = v.Status,
                RoomType = v.RoomTypes.FirstOrDefault(i => i.Id == v.RoomTypeId)
            };
        }
    }
}
