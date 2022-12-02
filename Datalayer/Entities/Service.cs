using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Entities
{
    [Table("Services")]
    public class Service : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string? Name { get; set; }
        [Required]
        public decimal Price { get; set; }

        //FK
        [Required]
        public int ReceiptId { get; set; }
    }
}
