using System.ComponentModel.DataAnnotations;

namespace Datalayer.Entities
{
    public class BaseEntity
    {
        [Key, Required]
        public int Id { get; set; }
    }
}
