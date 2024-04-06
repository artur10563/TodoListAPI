using Microsoft.EntityFrameworkCore;
using Todo.Application.Repositories.Shared;
using Todo.Domain.Entities.Shared;
using Todo.Infrastructure.Data;

namespace Todo.Infrastructure.Repositories.Shared
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


        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).AsQueryable().AsNoTracking();
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        public virtual TEntity? FirstOrDefault(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>()
                .Where(predicate)
                .FirstOrDefault();
        }

        public virtual void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }


        public virtual void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

    }
}