using Todo.Application.Repositories.Shared;
using Todo.Domain.Entities;

namespace Todo.Application.Repositories
{
	public interface ITodoTaskRepository : IBaseRepository<TodoTask>
	{
	}
}
