using System.ComponentModel.DataAnnotations;

namespace TodoApp.Client.Models
{
	public class UserLoginDto
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;

		[Required]
		[StringLength(50, ErrorMessage = "Password must be at least 7 characters long.", MinimumLength = 7)]
		public string Password { get; set; } = string.Empty;
	}
}
