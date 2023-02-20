using Entities.Interfaces;

namespace TodoApp.Server.Models
{
	public class TokenPattern : ITokenPattern
	{
		public string Pattern { get; set; }

		public TokenPattern(IConfiguration configuration)
		{
			Pattern = configuration.GetSection("AppSettings:Token").Value;
		}
	}
}
