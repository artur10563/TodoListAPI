using Todo.Domain.Entities.Shared;

namespace Todo.Application.Repositories.Shared
{
	public interface IBaseRepository<TEntity> where TEntity : EntityBase
	{
		void Add(TEntity entity);
		void AddRange(IEnumerable<TEntity> entities);

		IEnumerable<TEntity> GetAll();
		IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate);

		Task<TEntity?> GetByIdAsync(int id);

		void Remove(TEntity entity);
		void RemoveRange(IEnumerable<TEntity> entities);

		void Update(TEntity entity);

		TEntity? FirstOrDefault(Func<TEntity, bool> predicate);
	}
}
