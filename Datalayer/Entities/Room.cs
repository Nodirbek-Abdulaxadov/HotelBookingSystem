using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Entities
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
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
