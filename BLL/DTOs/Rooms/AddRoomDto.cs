using Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs.Rooms
{
    public class AddRoomDto
    {
        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty;
        [Required]
        public int Number { get; set; }
        [Required]
        public RoomStatus Status { get; set; }
        [Required]
        public int Capacity { get; set; }
        [StringLength(2000)]
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public IFormFile? ImageFile { get; set; }

        public static explicit operator Room(AddRoomDto v)
            => new Room()
            {
                Number = v.Number,
                Description = v.Description,
                Capacity = v.Capacity,
                Price = v.Price,
                Status = v.Status,
                Type = v.Type,
                ImagePath = ""
            };
    }
}
