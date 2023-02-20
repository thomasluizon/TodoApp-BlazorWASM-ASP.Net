using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public abstract class Entity
    {
        [Required]
        [Key]
        public Guid Id { get; set; }
    }
}
