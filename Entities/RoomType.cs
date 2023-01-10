using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    [Table("RoomTypes")]
    public class RoomType : BaseEntity
    {
        public RoomType()
        {
            Images = new List<string>();
            Rooms = new List<Room>();
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Capacity { get; set; }
        [StringLength(2000)]
        public string? DescriptionUz { get; set; }
        [StringLength(2000)]
        public string? DescriptionRu { get; set; }
        [StringLength(2000)]
        public string? DescriptionEn { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public List<string> Images { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
