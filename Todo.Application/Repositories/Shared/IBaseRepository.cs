using Todo.Domain.Entities.Shared;

namespace Todo.Application.Repositories.Shared
{
	public interface IBaseRepository<TEntity> where TEntity : EntityBase
	{
		void Add(TEntity entity);
	}
}
