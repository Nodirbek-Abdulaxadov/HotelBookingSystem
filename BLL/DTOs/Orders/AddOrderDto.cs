using Entities;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs.Orders
{
    public class AddOrderDto
    {
        [Required]
        [StringLength(100)]
        public string StartDate { get; set; }
        [Required]
        [StringLength(100)]
        public string EndDate { get; set; }
        [Required]
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        [StringLength(200)]
        public string Additional { get; set; }
        public decimal TotalPrice { get; set; }

        //FK
        [Required]
        public int RoomId { get; set; }
        public string GuestId { get; set; }

        public static explicit operator Order(AddOrderDto v)
            => new Order()
            {
                StartDate = v.StartDate,
                EndDate = v.EndDate,
                TotalPrice = v.TotalPrice,
                NumberOfChildren = v.NumberOfChildren,
                NumberOfAdults = v.NumberOfAdults,
                OrderStatus = OrderStatus.Waiting,
                Additional = v.Additional,
                BookedDate = DateTime.UtcNow.ToString(),
                RoomTypeId = v.RoomId,
                GuestId = v.GuestId
            };
    }
}
