using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Datalayer.Entities
{
    public class User :  IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string? FirstName { get; set; }
        [StringLength(50)]
        public string? LastName { get; set;}
    }
}
