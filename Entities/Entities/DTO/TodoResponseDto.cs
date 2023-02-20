using System.ComponentModel.DataAnnotations;

namespace Entities.Entities.DTO
{
	public class TodoResponseDto
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		public string? Title { get; set; }
	}
}
