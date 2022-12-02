using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Entities
{
    [Table("Images")]
    public class ImageModel : BaseEntity
    {
        [Required]
        [StringLength(500)]
        public string Link { get; set; } = string.Empty;

        //FK
        [Required]
        public int RoomId { get; set; }
    }
}
