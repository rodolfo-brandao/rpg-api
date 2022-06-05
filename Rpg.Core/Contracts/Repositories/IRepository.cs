using Rpg.Core.Models.Abstract;
using System.Linq.Expressions;

namespace Rpg.Core.Contracts.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetByKeyAsync(params object[] keyValues);
        Task<TEntity> InsertAsync(TEntity entity);
        Task InsertRangeAsync(params TEntity[] entities);
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, string includes = "", bool isReadOnly = false);
        TEntity Remove(TEntity entity);
        void RemoveRange(params TEntity[] entities);
        TEntity Update(TEntity entity);
        void UpdateRange(params TEntity[] entities);
    }
}
