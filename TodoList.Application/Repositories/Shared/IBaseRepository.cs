using TodoList.Domain.Entities.Shared;

namespace TodoList.Application.Repositories.Shared
{
	public interface IBaseRepository<TEntity> where TEntity : EntityBase
	{
		void Add(TEntity entity);
	}
}
