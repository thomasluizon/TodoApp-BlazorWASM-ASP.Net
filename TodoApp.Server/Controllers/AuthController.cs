using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using TodoApp.Server.Models;
using TodoApp.Server.Models.DTO;

namespace TodoApp.Server.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		public static User user = new();

		public AuthController(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		[HttpPost]
		public async Task<ActionResult<User>> Register(UserRegisterDto request)
		{
			CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

			user.Id = Guid.NewGuid();
			user.Name = request.Name;
			user.Email = request.Email;
			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordSalt;

			return Ok(user);
		}

		[HttpPost]
		public async Task<ActionResult<string>> Login(UserLoginDto request)
		{
			if (!user.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase))
				return BadRequest("User not found.");

			if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
				return BadRequest("Incorrect password");

			return Ok(CreateToken(user));
		}

		private string CreateToken(User userRequest)
		{
			List<Claim> claims = new()
			{
				new Claim(ClaimTypes.Email, userRequest.Email)
			};

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
			var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
			var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: cred);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using var hmac = new HMACSHA512();
			passwordSalt = hmac.Key;
			passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
		}

		private static bool VerifyPasswordHash(string password, IEnumerable<byte> passwordHash, byte[] passwordSalt)
		{
			using var hmac = new HMACSHA512(passwordSalt);
			var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			return computedHash.SequenceEqual(passwordHash);
		}
	}
}
