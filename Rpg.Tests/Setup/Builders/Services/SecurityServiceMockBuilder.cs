using Moq;
using Rpg.Core.Contracts.Services;

namespace Rpg.Tests.Setup.Builders.Services
{
    internal sealed class SecurityServiceMockBuilder
    {
        private readonly Mock<ISecurityService> _mock;

        private SecurityServiceMockBuilder()
        {
            _mock = new Mock<ISecurityService>();
        }

        public ISecurityService Build()
        {
            return _mock.Object;
        }

        public static SecurityServiceMockBuilder Create()
        {
            return new SecurityServiceMockBuilder();
        }

        public SecurityServiceMockBuilder SetupValidatePassword(bool passwordIsValid = false)
        {
            _mock.Setup(service => service.ValidatePassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(passwordIsValid);
            return this;
        }
    }
}
