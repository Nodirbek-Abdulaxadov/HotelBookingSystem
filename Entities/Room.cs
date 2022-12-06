using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Rooms")]
    public class Room : BaseEntity
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
        public string? DescriptionUz { get; set; }
        [StringLength(2000)]
        public string? DescriptionRu { get; set; }
        [StringLength(2000)]
        public string? DescriptionEn { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string? ImagePath { get; set; }
    }
}
