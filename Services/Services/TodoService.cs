using Entities;
using Entities.Entities;
using Entities.Entities.DTO;
using Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Services.Services
{
	public class TodoService : ITodoService
	{
		private readonly ApplicationDbContext _db;

		public TodoService(ApplicationDbContext context)
		{
			_db = context;
		}

		public async Task<IEnumerable<TodoResponseDto>> GetAll(Guid userId)
		{
			var todos = await GetUserTodos(userId);

			return todos.Select(todo => new TodoResponseDto() { Id = todo.Id, Title = todo.Title });
		}

		public async Task<TodoResponseDto?> GetById(Guid id, Guid userId)
		{
			var todos = await GetUserTodos(userId);
			var todo = todos.FirstOrDefault(todo => todo.Id == id);

			return todo == null ? null : new TodoResponseDto() { Id = todo.Id, Title = todo.Title };
		}

		public async Task<TodoResponseDto?> Create(TodoCreateRequestDto todoRequest, Guid userId)
		{
			var user = await _db.Users.FindAsync(userId);

			if (user == null)
				return null;

			var todo = new Todo()
			{
				Id = Guid.NewGuid(),
				Title = todoRequest.Title,
				UserId = userId,
				User = user
			};

			await _db.Todos.AddAsync(todo);
			await _db.SaveChangesAsync();

			return new TodoResponseDto() { Id = todo.Id, Title = todo.Title };
		}

		public async Task<TodoResponseDto?> Update(TodoUpdateRequestDto todoRequest, Guid userId)
		{
			var todos = await GetUserTodos(userId);
			var todoToUpdate = todos.FirstOrDefault(todo => todo.Id == todoRequest.Id);

			if (todoToUpdate == null)
				return null;

			todoToUpdate.Title = todoRequest.Title;
			await _db.SaveChangesAsync();

			return new TodoResponseDto()
			{
				Id = todoToUpdate.Id,
				Title = todoToUpdate.Title
			};
		}

		public async Task<bool> Delete(Guid id, Guid userId)
		{
			var todos = await GetUserTodos(userId);
			var todoToRemove = todos.FirstOrDefault(todo => todo.Id == id);

			if (todoToRemove == null)
				return false;

			_db.Remove(todoToRemove);
			await _db.SaveChangesAsync();

			return true;
		}

		public async Task<IEnumerable<Todo>> GetUserTodos(Guid userId)
		{
			return await _db.Todos.Where(todo => todo.UserId == userId).ToListAsync();
		}
	}
}
