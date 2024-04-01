using TodoList.Application.Repositories.Shared;
using TodoList.Domain.Entities.Shared;

namespace TodoList.Infrastructure.Data.Repositories.Shared
{
	abstract public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : EntityBase
	{
		protected readonly AppDbContext _context;
		protected BaseRepository(AppDbContext context)
		{
			_context = context;
		}

		public void Add(TEntity entity)
		{
			_context.Set<TEntity>().Add(entity);
		}
	}
}