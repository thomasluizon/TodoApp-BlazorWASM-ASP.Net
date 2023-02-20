using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
	public class Todo : Entity
	{
		[Required]
		public string? Title { get; set; }

		[ForeignKey("User")]
		[Required]
		public Guid UserId { get; set; }
		public User? User { get; set; }
	}
}
