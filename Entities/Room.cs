using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Rooms")]
    public class Room : BaseEntity
    {
        [Required]
        public int Number { get; set; }
        [Required]
        public RoomStatus Status { get; set; }

        public RoomType RoomType { get; set; }
        [ForeignKey("OrderrId")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
