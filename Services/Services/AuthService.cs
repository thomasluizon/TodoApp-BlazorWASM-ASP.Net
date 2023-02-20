using Entities;
using Entities.Entities;
using Entities.Entities.DTO;
using Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Services.Services
{
	public class AuthService : IAuthService
	{
		private readonly ApplicationDbContext _db;
		private readonly string _pattern;

		public AuthService(ApplicationDbContext dbContext, ITokenPattern pattern)
		{
			_pattern = pattern.Pattern;
			_db = dbContext;
		}

		public async Task<User> Register(UserRegisterDto request)
		{
			CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

			var user = new User
			{
				Id = Guid.NewGuid(),
				Name = request.Name,
				Email = request.Email,
				PasswordHash = passwordHash,
				PasswordSalt = passwordSalt
			};

			await _db.Users.AddAsync(user);
			await _db.SaveChangesAsync();

			return user;
		}

		public async Task<string?> Login(UserLoginDto request)
		{
			var user = await _db.Users.SingleOrDefaultAsync(user => request.Email.Equals(user.Email));

			if (user == null)
				return null;

			if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
				return null;

			var token = CreateToken(user);

			return token;
		}

		/// <summary>
		/// Creates a Json Web Token for the user
		/// </summary>
		/// <param name="userRequest">Receives the userRequest object</param>
		/// <returns>Returns the Json Web Token</returns>
		private string CreateToken(Entity userRequest)
		{
			List<Claim> claims = new()
			{
				new Claim("id", userRequest.Id.ToString())
			};

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_pattern));
			var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
			var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: cred);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		/// <summary>
		/// Creates a password hash to store in the database
		/// </summary>
		/// <param name="password">Receives the password from the user</param>
		/// <param name="passwordHash">Receives the passwordHash array that will be stored the passwordHash</param>
		/// <param name="passwordSalt">Receives the passwordSalt array that will be stored the passwordSalt</param>
		private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using var hmac = new HMACSHA512();
			passwordSalt = hmac.Key;
			passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
		}

		/// <summary>
		/// Checks if the user password is correct
		/// </summary>
		/// <param name="password">Receives the user password</param>
		/// <param name="passwordHash">Receives the user password hash</param>
		/// <param name="passwordSalt">Receives the password salt</param>
		/// <returns>Returns true if user password is correct</returns>
		private static bool VerifyPasswordHash(string password, IEnumerable<byte> passwordHash, byte[] passwordSalt)
		{
			using var hmac = new HMACSHA512(passwordSalt);
			var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			return computedHash.SequenceEqual(passwordHash);
		}
	}
}
