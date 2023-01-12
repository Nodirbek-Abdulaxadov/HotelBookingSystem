using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Orders")]
    public class Order : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string StartDate { get; set; }
        [Required]
        [StringLength(100)]
        public string EndDate { get; set;}
        [Required]
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        [StringLength(200)]
        public string Additional { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [StringLength(100)]
        public string BookedDate { get; set; }
        [StringLength(100)]
        public string ConfirmedDate { get; set; }
        [Required]
        public OrderStatus OrderStatus { get; set; }

        //FK
        public List<Room> Rooms { get; set; }
        public int RoomId { get; set; }
        public int RoomTypeId { get; set; }
        [Required]
        public string GuestId { get; set; }
    }
}
