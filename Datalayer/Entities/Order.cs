using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Entities
{
    [Table("Orders")]
    public class Order : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string? StartDate { get; set; }
        [Required]
        [StringLength(100)]
        public string? EndDate { get; set;}
        [Required]
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        [StringLength(200)]
        public string? Additional { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [StringLength(100)]
        public string? BookedDate { get; set; }
        [StringLength(100)]
        public string? ConfirmedDate { get; set; }
        [Required]
        public OrderStatus OrderStatus { get; set; }

        //FK
        [Required]
        public int RoomId { get; set; }
        [Required]
        public int GuestId { get; set; }
    }
}
