using Entities.Entities;
using Entities.Entities.DTO;
using Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Server.Controllers
{
    public class AuthController : BaseController
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost]
		public async Task<ActionResult<User>> Register(UserRegisterDto request)
		{
			try
			{
				var user = await _authService.Register(request);

				if (!ModelState.IsValid)
					return BadRequest("Wrong user information.");

				return Ok(user);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e);
			}
		}

		[HttpPost]
		public async Task<ActionResult<string>> Login(UserLoginDto request)
		{
			if (!ModelState.IsValid)
				return BadRequest("Wrong user information.");
			try
			{
				var token = await _authService.Login(request);

				if (string.IsNullOrEmpty(token))
					return BadRequest("Incorrect email or password.");

				return Ok(token);
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e);
			}
		}
	}
}
