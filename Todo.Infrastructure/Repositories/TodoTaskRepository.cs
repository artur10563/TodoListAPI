using Microsoft.EntityFrameworkCore;
using Todo.Application.Repositories;
using Todo.Domain.Entities;
using Todo.Infrastructure.Data;
using Todo.Infrastructure.Repositories.Shared;

namespace Todo.Infrastructure.Repositories
{
	public class TodoTaskRepository : BaseRepository<TodoTask>, ITodoTaskRepository
	{
		public TodoTaskRepository(AppDbContext context) : base(context)
		{
		}
	}
}
