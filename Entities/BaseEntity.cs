using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class BaseEntity
    {
        [Key, Required]
        public int Id { get; set; }
    }
}
