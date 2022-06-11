using Microsoft.EntityFrameworkCore;
using Rpg.Core.Contracts.Repositories;
using Rpg.Core.Models.Abstract;
using Rpg.Data.Context;
using System.Linq.Expressions;

namespace Rpg.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly DbSet<TEntity> _entities;

        public Repository(RpgContext rpgContext)
        {
            _entities = rpgContext.Set<TEntity>();
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Query(expression, isReadOnly: true).AnyAsync();
        }

        public async Task<TEntity> GetByKeyAsync(params object[] keyValues)
        {
            return await _entities.FindAsync(keyValues);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            return (await _entities.AddAsync(entity)).Entity;
        }

        public async Task InsertRangeAsync(params TEntity[] entities)
        {
            await _entities.AddRangeAsync(entities);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, string includes = "", bool isReadOnly = false)
        {
            var query = isReadOnly ? _entities.AsNoTracking() : _entities;

            foreach (var included in includes.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(included);
            }

            return query.Where(expression ?? (_ => true));
        }

        public TEntity Remove(TEntity entity)
        {
            return _entities.Remove(entity).Entity;
        }

        public void RemoveRange(params TEntity[] entities)
        {
            _entities.RemoveRange(entities);
        }

        public TEntity Update(TEntity entity)
        {
            return _entities.Update(entity).Entity;
        }

        public void UpdateRange(params TEntity[] entities)
        {
            _entities.UpdateRange(entities);
        }
    }
}
