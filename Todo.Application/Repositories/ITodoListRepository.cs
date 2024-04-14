using Todo.Application.Repositories.Shared;
using Todo.Domain.Entities;

namespace Todo.Application.Repositories
{
	public interface ITodoListRepository : IBaseRepository<TodoList>
	{
		Task<bool> IsTitleUniqueForUser(int userId, string title);
		IEnumerable<TodoList> GetAllWithIncludes();
		IEnumerable<TodoList> GetAllWithIncludes(Func<TodoList, bool> predicate);

		
	}
}
