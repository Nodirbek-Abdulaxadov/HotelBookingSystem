﻿using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Receipt : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string? CheckoutDate { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }

        //FK
        [Required]
        public string GuestId { get; set; }
        [Required]
        public int OrdeId { get; set; }
        [Required]
        public string AdminId { get; set; }

    }
}
