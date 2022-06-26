using Moq;
using Rpg.Core.Contracts.UnitsOfWork;

namespace Rpg.Tests.Setup.Builders.UnitsOfWork
{
    internal sealed class UnitOfWorkMockBuilder
    {
        private readonly Mock<IUnitOfWork> _mock;

        private UnitOfWorkMockBuilder()
        {
            _mock = new Mock<IUnitOfWork>();
        }

        public IUnitOfWork Build()
        {
            return _mock.Object;
        }

        public static UnitOfWorkMockBuilder Create()
        {
            return new UnitOfWorkMockBuilder();
        }

        public UnitOfWorkMockBuilder SetupSaveChanges()
        {
            _mock.Setup(unitOfWork => unitOfWork.SaveChangesAsync()).ReturnsAsync(default(int));
            return this;
        }
    }
}
