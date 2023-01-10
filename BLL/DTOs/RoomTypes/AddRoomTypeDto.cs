using Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs.RoomTypes
{
    public class AddRoomTypeDto
    {
        public AddRoomTypeDto()
        {
            ImageFiles = new List<IFormFile>();
        }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Capacity { get; set; }
        [Required]
        [StringLength(2000)]
        public string DescriptionUz { get; set; }
        [Required]
        [StringLength(2000)]
        public string DescriptionRu { get; set; }
        [Required]
        [StringLength(2000)]
        public string DescriptionEn { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public List<IFormFile> ImageFiles { get; set; }

        public static explicit operator RoomType(AddRoomTypeDto v)
            => new RoomType()
            {
                DescriptionUz = v.DescriptionUz,
                DescriptionRu = v.DescriptionRu,
                DescriptionEn = v.DescriptionEn,
                Capacity = v.Capacity,
                Price = v.Price,
                Name = v.Name
            };
    }
}
