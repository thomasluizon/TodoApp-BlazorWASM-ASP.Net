using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Server.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public abstract class BaseController : ControllerBase
	{
	}
}
