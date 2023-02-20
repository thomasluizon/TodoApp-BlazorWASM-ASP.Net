using Entities.Entities;
using Entities.Entities.DTO;

namespace Entities.Interfaces
{
	public interface IAuthService
	{
		/// <summary>
		/// Register an user to the database
		/// </summary>
		/// <param name="request">Receives the UserRegisterDto object to register</param>
		/// <returns>Returns the User object that was registered</returns>
		Task<User> Register(UserRegisterDto request);

		/// <summary>
		/// Login the user
		/// </summary>
		/// <param name="request">Receives the UserLoginDto object to login</param>
		/// <returns>Returns the generated user Json Web Token</returns>
		Task<string?> Login(UserLoginDto request);
	}
}
