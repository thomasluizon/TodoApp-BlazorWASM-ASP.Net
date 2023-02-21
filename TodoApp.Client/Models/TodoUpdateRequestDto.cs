using System.ComponentModel.DataAnnotations;

namespace Entities.Entities.DTO
{
	public class TodoUpdateRequestDto
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		public string? Title { get; set; }
	}
}
