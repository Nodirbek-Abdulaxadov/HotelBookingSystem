using Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs.RoomTypes
{
    public class UpdateRoomTypeDto
    {
        public UpdateRoomTypeDto()
        {
            NewImageFiles = new List<IFormFile>();
            Images = new List<string>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [StringLength(2000)]
        public string DescriptionUz { get; set; }
        [StringLength(2000)]
        public string DescriptionRu { get; set; }
        [StringLength(2000)]
        public string DescriptionEn { get; set; }
        public List<IFormFile> NewImageFiles { get; set; }
        public List<string> Images { get; set; }

        public static explicit operator RoomType(UpdateRoomTypeDto v)
            => new RoomType()
            {
                DescriptionUz = v.DescriptionUz,
                DescriptionRu = v.DescriptionRu,
                DescriptionEn = v.DescriptionEn,
                Capacity = v.Capacity,
                Price = v.Price,
                Name = v.Name,
                Id = v.Id,
                Images = v.Images
            };
    }
}
