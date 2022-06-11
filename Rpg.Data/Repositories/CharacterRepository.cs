using Microsoft.EntityFrameworkCore;
using Rpg.Core.Contracts.Repositories;
using Rpg.Core.Models;
using Rpg.Core.Models.Nulls;
using System.Linq.Expressions;

namespace Rpg.Data.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IRepository<Character> _repository;

        public CharacterRepository(IRepository<Character> repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExistsAsync(Expression<Func<Character, bool>> expression)
        {
            return await _repository.ExistsAsync(expression);
        }

        public async Task<IReadOnlyList<Character>> GetAllAsync(string includes = "", bool isReadOnly = false)
        {
            return await _repository.Query(character => !character.IsDisabled, includes, isReadOnly).ToListAsync();
        }

        public async Task<Character> GetByIdAsync(Guid id)
        {
            return await _repository.GetByKeyAsync(id) ?? new NullCharacter();
        }

        public async Task<Character> InsertAsync(Character character)
        {
            return await _repository.InsertAsync(character);
        }

        public async Task InsertRangeAsync(params Character[] characters)
        {
            await _repository.InsertRangeAsync(characters);
        }

        public IQueryable<Character> Query(Expression<Func<Character, bool>> expression, string includes = "", bool isReadOnly = false)
        {
            return _repository.Query(expression, includes, isReadOnly);
        }

        public Character Remove(Character character)
        {
            return _repository.Remove(character);
        }

        public void RemoveRange(params Character[] characters)
        {
            _repository.RemoveRange(characters);
        }

        public Character Update(Character character)
        {
            return _repository.Update(character);
        }

        public void UpdateRange(params Character[] characters)
        {
            _repository.UpdateRange(characters);
        }
    }
}
