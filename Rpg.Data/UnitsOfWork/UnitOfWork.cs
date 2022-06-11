using Rpg.Core.Contracts.UnitsOfWork;
using Rpg.Data.Context;

namespace Rpg.Data.UnitsOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly RpgContext _rpgContext;

        public UnitOfWork(RpgContext rpgContext)
        {
            _rpgContext = rpgContext;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _rpgContext.SaveChangesAsync();
        }
    }
}
