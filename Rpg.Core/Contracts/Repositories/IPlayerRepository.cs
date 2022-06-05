using Rpg.Core.Models;
using System.Linq.Expressions;

namespace Rpg.Core.Contracts.Repositories
{
    public interface IPlayerRepository
    {
        Task<bool> ExistsAsync(Expression<Func<Player, bool>> expression);
        Task<IReadOnlyList<Player>> GetAllAsync(string includes = "", bool isReadOnly = false);
        Task<Player> GetByIdAsync(Guid id);
        Task<Player> GetByUsernameAsync(string username, string includes = "", bool isReadOnly = false);
        Task<Player> InsertAsync(Player player);
        Task InsertRangeAsync(params Player[] players);
        IQueryable<Player> Query(Expression<Func<Player, bool>> expression, string includes = "", bool isReadOnly = false);
        Player Remove(Player player);
        void RemoveRange(params Player[] players);
        Player Update(Player player);
        void UpdateRange(params Player[] players);
    }
}
