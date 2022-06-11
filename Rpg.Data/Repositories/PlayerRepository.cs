using Microsoft.EntityFrameworkCore;
using Rpg.Core.Contracts.Repositories;
using Rpg.Core.Models;
using Rpg.Core.Models.Nulls;
using System.Linq.Expressions;

namespace Rpg.Data.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IRepository<Player> _repository;

        public PlayerRepository(IRepository<Player> repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExistsAsync(Expression<Func<Player, bool>> expression)
        {
            return await _repository.ExistsAsync(expression);
        }

        public async Task<IReadOnlyList<Player>> GetAllAsync(string includes = "", bool isReadOnly = false)
        {
            return await _repository.Query(player => !player.IsDisabled, includes, isReadOnly).ToListAsync();
        }

        public async Task<Player> GetByIdAsync(Guid id)
        {
            return await _repository.GetByKeyAsync(id) ?? new NullPlayer();
        }

        public async Task<Player> GetByUsernameAsync(string username, string includes = "", bool isReadOnly = false)
        {
            return await _repository.Query(player => player.Username.Equals(username), includes, isReadOnly).FirstOrDefaultAsync() ?? new NullPlayer();
        }

        public async Task<Player> InsertAsync(Player player)
        {
            return await _repository.InsertAsync(player);
        }

        public async Task InsertRangeAsync(params Player[] players)
        {
            await _repository.InsertRangeAsync(players);
        }

        public IQueryable<Player> Query(Expression<Func<Player, bool>> expression, string includes = "", bool isReadOnly = false)
        {
            return _repository.Query(expression, includes, isReadOnly);
        }

        public Player Remove(Player player)
        {
            return _repository.Remove(player);
        }

        public void RemoveRange(params Player[] players)
        {
            _repository.RemoveRange(players);
        }

        public Player Update(Player player)
        {
            return _repository.Update(player);
        }

        public void UpdateRange(params Player[] players)
        {
            _repository.UpdateRange(players);
        }
    }
}
