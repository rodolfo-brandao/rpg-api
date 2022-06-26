using Moq;
using Rpg.Core.Contracts.Repositories;
using Rpg.Core.Models;
using Rpg.Core.Models.Nulls;
using System.Linq.Expressions;

namespace Rpg.Tests.Setup.Builders.Repositories
{
    internal sealed class PlayerRepositoryMockBuilder
    {
        private readonly Mock<IPlayerRepository> _mock;

        private PlayerRepositoryMockBuilder()
        {
            _mock = new Mock<IPlayerRepository>();
        }

        public IPlayerRepository Build()
        {
            return _mock.Object;
        }

        public static PlayerRepositoryMockBuilder Create()
        {
            return new PlayerRepositoryMockBuilder();
        }

        public PlayerRepositoryMockBuilder SetupGetByUsername(Player player = default)
        {
            _mock.Setup(repository => repository.GetByUsernameAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync(player ?? new NullPlayer());
            return this;
        }

        public PlayerRepositoryMockBuilder SetupExists(bool exists = default)
        {
            _mock.Setup(repository => repository.ExistsAsync(It.IsAny<Expression<Func<Player, bool>>>())).ReturnsAsync(exists);
            return this;
        }
    }
}
