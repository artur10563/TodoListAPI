using Todo.Application.Repositories;
using Todo.Infrastructure.Data;

namespace Todo.Infrastructure.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _context;
		public UnitOfWork(AppDbContext context)
		{
			_context = context;
		}

		public int SaveChanges()
		{
			return _context.SaveChanges();
		}

		public Task<int> SaveChangesAsync()
		{
			return _context.SaveChangesAsync();
		}
		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
