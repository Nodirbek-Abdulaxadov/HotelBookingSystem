using Entities;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs.Services
{
    public class AddServiceDto
    {
        [Required]
        [StringLength(200)]
        public string? Name { get; set; }
        [Required]
        public decimal Price { get; set; }

        //FK
        [Required]
        public int ReceiptId { get; set; }

        public static explicit operator Service(AddServiceDto v)
            => new Service()
            {
                Name = v.Name,
                Price = v.Price,
                ReceiptId = v.ReceiptId
            };
    }
}
