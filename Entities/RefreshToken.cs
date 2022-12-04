using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("RefreshTokens")]
    public class RefreshToken : BaseEntity
    {
        public string? Token { get; set; }
        public string? JwtId { get; set; }
        public bool IsRevoked { get; set; }
        [StringLength(50)]
        public string? DateAdded { get; set; }
        [StringLength(50)]
        public string? DataExpire { get; set; }

        public Guid UserId { get; set; }
    }
}
