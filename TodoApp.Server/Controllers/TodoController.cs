using Entities.Entities.DTO;
using Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.Extensions;

namespace TodoApp.Server.Controllers
{
	[Route("api/[controller]")]
	public class TodoController : BaseController
	{
		private readonly ITodoService _todoService;
		public TodoController(ITodoService todoService)
		{
			_todoService = todoService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var userId = HttpContext.GetUserId();

			if (userId == null)
				return Unauthorized("User must be logged in");

			var todos = await _todoService.GetAll((Guid)userId);
			return new JsonResult(todos);
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			var userId = HttpContext.GetUserId();

			if (userId == null)
				return Unauthorized("User must be logged in");

			var todo = await _todoService.GetById(id, (Guid)userId);

			if (todo == null)
				return NotFound("Todo not found");

			return new JsonResult(todo);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] TodoCreateRequestDto todoCreateRequest)
		{
			if (!ModelState.IsValid)
				return BadRequest("Please supply a valid user");

			var userId = HttpContext.GetUserId();

			if (userId == null)
				return Unauthorized("User must be logged in");

			var todoResponse = await _todoService.Create(todoCreateRequest, (Guid)userId);

			if (todoResponse == null)
				return StatusCode(500, "There was an error creating the todo");

			return new JsonResult(todoResponse);
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] TodoUpdateRequestDto todoUpdateRequest)
		{
			if (!ModelState.IsValid)
				return BadRequest("Please supply a valid user");

			var userId = HttpContext.GetUserId();

			if (userId == null)
				return Unauthorized("User must be logged in");

			var todoResponse = await _todoService.Update(todoUpdateRequest, (Guid)userId);

			if (todoResponse == null)
				return StatusCode(500, "There was an error creating the todo");

			return new JsonResult(todoResponse);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var userId = HttpContext.GetUserId();

			if (userId == null)
				return Unauthorized("User must be logged in");

			var isDeleted = await _todoService.Delete(id, (Guid)userId);

			return isDeleted ? Ok("Todo has been deleted") : BadRequest("Todo was not deleted");
		}
	}
}
