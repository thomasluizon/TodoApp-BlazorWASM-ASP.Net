using Entities.Entities.DTO;

namespace Entities.Interfaces
{
	public interface ITodoService
	{
		/// <summary>
		/// Gets all the user's todos
		/// </summary>
		/// <param name="userId">Receives the user id</param>
		/// <returns>Returns a list of todos</returns>
		Task<IEnumerable<TodoResponseDto>> GetAll(Guid userId);

		/// <summary>
		/// Gets an specific todo by the id
		/// </summary>
		/// <param name="id">Receives the todo id</param>
		/// <param name="userId">Receives the user id</param>
		/// <returns>Returns the matching todo</returns>
		Task<TodoResponseDto?> GetById(Guid id, Guid userId);

		/// <summary>
		/// Creates a new todo
		/// </summary>
		/// <param name="todoRequest">Receives the todo request object</param>
		/// <param name="userId">Receives the user id</param>
		/// <returns>Returns the created todo</returns>
		Task<TodoResponseDto?> Create(TodoCreateRequestDto todoRequest, Guid userId);

		/// <summary>
		/// Updates an specific todo
		/// </summary>
		/// <param name="todoRequest">Receives the todo request object</param>
		/// <param name="userId">Receives the user id</param>
		/// <returns>Returns the updated todo</returns>
		Task<TodoResponseDto?> Update(TodoUpdateRequestDto todoRequest, Guid userId);

		/// <summary>
		/// Deletes a todo from the database
		/// </summary>
		/// <param name="id">Receives the todo id</param>
		/// <param name="userId">Receives the user id</param>
		/// <returns>Returns true if the todo was successfully deleted</returns>
		Task<bool> Delete(Guid id, Guid userId);
	}
}
