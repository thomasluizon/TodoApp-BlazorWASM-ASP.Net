using System.ComponentModel.DataAnnotations;

namespace Entities.Entities.DTO
{
	public class TodoCreateRequestDto
	{
		[Required]
		public string? Title { get; set; }
	}
}
