using Entities;

namespace BLL.DTOs.Rooms
{
    public class ViewRoomDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Status { get; set; }
        public string RoomTypeName { get; set; }

        public static explicit operator ViewRoomDto(Room v)
        {
            return new ViewRoomDto()
            {
                Id = v.Id,
                Number = v.Number,
                RoomTypeName = v.RoomType.Name,
                Status = ConvertStatus(v.Status)
            };
        }

        public static string ConvertStatus(RoomStatus status)
            => status switch
            {
                RoomStatus.Unknown => "Unknown",
                RoomStatus.Busy => "Busy",
                RoomStatus.Empty => "Empty",
                RoomStatus.Ordered => "Ordered",
                _ => ""
            };
    }
}
