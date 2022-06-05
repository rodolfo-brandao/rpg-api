using Rpg.Core.Models;
using System.Linq.Expressions;

namespace Rpg.Core.Contracts.Repositories
{
    public interface ICharacterRepository
    {
        Task<bool> ExistsAsync(Expression<Func<Character, bool>> expression);
        Task<IReadOnlyList<Character>> GetAllAsync(string includes = "", bool isReadOnly = false);
        Task<Character> GetByIdAsync(Guid id);
        Task<Character> InsertAsync(Character character);
        Task InsertRangeAsync(params Character[] characters);
        IQueryable<Character> Query(Expression<Func<Character, bool>> expression, string includes = "", bool isReadOnly = false);
        Character Remove(Character character);
        void RemoveRange(params Character[] characters);
        Character Update(Character character);
        void UpdateRange(params Character[] characters);
    }
}
