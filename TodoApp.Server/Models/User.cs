namespace TodoApp.Server.Models
{
	public class User
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
	}
}
