using Todo.Application.Repositories.Shared;
using Todo.Domain.Entities.Shared;

namespace Todo.Infrastructure.Data.Repositories.Shared
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