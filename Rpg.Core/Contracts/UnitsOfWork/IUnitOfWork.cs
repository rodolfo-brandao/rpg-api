namespace Rpg.Core.Contracts.UnitsOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
