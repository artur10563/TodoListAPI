using Microsoft.EntityFrameworkCore;
using Todo.Application.Repositories;
using Todo.Domain.Entities;
using Todo.Infrastructure.Data;
using Todo.Infrastructure.Repositories.Shared;

namespace Todo.Infrastructure.Repositories
{
	public class TodoListRepository : BaseRepository<TodoList>, ITodoListRepository
	{
		public TodoListRepository(AppDbContext context) : base(context) { }

		public IEnumerable<TodoList> GetAllWithIncludes()
		{
			return _context.TodoLists
				.Include(list => list.Owner)
				.Include(list => list.Tasks)
				.AsNoTracking();
		}

		public IEnumerable<TodoList> GetAllWithIncludes(Func<TodoList, bool> predicate)
		{
			return _context.TodoLists
				.Include(list => list.Owner)
				.Include(list => list.Tasks)
				.AsNoTracking()
				.Where(predicate);
		}

		public async Task<bool> IsTitleUniqueForUser(int userId, string title)
		{
			return !await _context.TodoLists
				.AnyAsync(list =>
				list.OwnerId == userId &&
				list.Title.ToLower() == title.ToLower());
		}
		public override Task<TodoList?> GetByIdAsync(int id)
		{
			return _context.TodoLists
				.Include(list => list.Tasks)
				.FirstOrDefaultAsync(list => list.Id == id);
		}
	}
}
