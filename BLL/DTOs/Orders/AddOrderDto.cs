using Entities;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs.Orders
{
    public class AddOrderDto
    {
        [Required]
        [StringLength(100)]
        public string? StartDate { get; set; }
        [Required]
        [StringLength(100)]
        public string? EndDate { get; set; }
        [Required]
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        [StringLength(200)]
        public string? Additional { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [StringLength(100)]
        public string? ConfirmedDate { get; set; }

        //FK
        [Required]
        public int RoomId { get; set; }
        [Required]
        public int GuestId { get; set; }

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
                RoomId = v.RoomId,
                GuestId = v.GuestId
            };
    }
}
