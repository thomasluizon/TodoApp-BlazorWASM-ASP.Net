using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class User : Entity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(300)]
        public byte[]? PasswordHash { get; set; }

        [Required]
        [MaxLength(300)]
        public byte[]? PasswordSalt { get; set; }
    }
}
