using Moq;
using Rpg.Application.Handlers.Auth;
using Rpg.Application.MapperProfiles;
using Rpg.Application.Requests.Auth;
using Rpg.Application.Responses;
using Rpg.Application.Responses.Auth;
using Rpg.Tests.Setup.Builders.Repositories;
using Rpg.Tests.Setup.Builders.Services;
using Rpg.Tests.Setup.Fakers.Models;
using Rpg.Tests.Setup.Fakers.Requests.Auth;
using Rpg.Tests.Setup.Helpers;
using Rpg.Tests.Setup.Providers.Requests.Auth;
using Serilog;

namespace Rpg.Tests.Application.Handlers.Auth
{
    [Trait("Application", "Handler")]
    public class LoginHandlerTest
    {
        private readonly Profile _entityToResponseProfile = new EntityToResponseProfile();
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public LoginHandlerTest()
        {
            _mapper = AutoMapperHelper.AddMappingProfile(_entityToResponseProfile);
            _logger = Mock.Of<ILogger>();
        }

        [Fact(DisplayName = $"{nameof(LoginHandler)} - Success")]
        public async Task HandleRequest_PassValidRequestObject_HandlerShouldLogIn()
        {
            // Arrange:
            var loginRequest = LoginRequestFake
                .Valid()
                .Generate();

            var player = PlayerFake
                .Valid(loginRequest.Username)
                .Generate();

            var playerRepository = PlayerRepositoryMockBuilder
                .Create()
                .SetupGetByUsername(player)
                .Build();

            var securityService = SecurityServiceMockBuilder
                .Create()
                .SetupCreatePlayerAccessToken()
                .SetupValidatePassword(passwordIsValid: true)
                .Build();

            var handler = new LoginHandler(playerRepository, securityService, _mapper, _logger);

            // Act:
            var result = await handler.Handle(loginRequest, cancellationToken: default);

            // Assert:
            result.Should().NotBeNull().And.BeOfType<ApiResult<LoginResponse>>();
            result.IsSuccessfulRequest.Should().BeTrue();
            result.Response.Should().NotBeNull().And.BeOfType<LoginResponse>();
            result.ErrorMessages.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = $"{nameof(LoginHandler)} - Fail: non-existent username")]
        public async Task HandleRequest_PassRequestObjectWithNonExistentUsername_HandlerShouldNotLogIn()
        {
            // Arrange:
            var loginRequest = LoginRequestFake
                .Valid()
                .Generate();

            var playerRepository = PlayerRepositoryMockBuilder
                .Create()
                .SetupGetByUsername()
                .Build();

            var securityService = SecurityServiceMockBuilder
                .Create()
                .Build();

            var handler = new LoginHandler(playerRepository, securityService, _mapper, _logger);

            // Act:
            var result = await handler.Handle(loginRequest, cancellationToken: default);

            // Assert:
            result.Should().NotBeNull().And.BeOfType<ApiResult<LoginResponse>>();
            result.IsSuccessfulRequest.Should().BeFalse();
            result.Response.Should().BeNull();
            result.ErrorMessages.Should().NotBeNullOrEmpty();

        }

        [Fact(DisplayName = $"{nameof(LoginHandler)} - Fail: invalid password")]
        public async Task HandleRequest_PassRequestObjectWithInvalidPassword_HandlerShouldNotLogIn()
        {
            // Arrange:
            var loginRequest = LoginRequestFake
                .Valid()
                .Generate();

            var player = PlayerFake
                .Valid(loginRequest.Username)
                .Generate();

            var playerRepository = PlayerRepositoryMockBuilder
                .Create()
                .SetupGetByUsername(player)
                .Build();

            var securityService = SecurityServiceMockBuilder
                .Create()
                .SetupValidatePassword()
                .Build();

            var handler = new LoginHandler(playerRepository, securityService, _mapper, _logger);

            // Act:
            var result = await handler.Handle(loginRequest, cancellationToken: default);

            // Assert:
            result.Should().NotBeNull().And.BeOfType<ApiResult<LoginResponse>>();
            result.IsSuccessfulRequest.Should().BeFalse();
            result.Response.Should().BeNull();
            result.ErrorMessages.Should().NotBeNullOrEmpty();
        }

        [Theory(DisplayName = $"{nameof(LoginHandler)} - Fail: no username and password")]
        [ClassData(typeof(LoginRequestWithInvalidData))]
        public async Task HandleRequest_PassEmpytRequestObject_HandlerShouldNotLogIn(LoginRequest loginRequest)
        {
            // Arrange:
            var player = PlayerFake
                .Valid()
                .Generate();

            var playerRepository = PlayerRepositoryMockBuilder
                .Create()
                .SetupGetByUsername(player)
                .Build();

            var securityService = SecurityServiceMockBuilder
                .Create()
                .SetupValidatePassword()
                .Build();

            var handler = new LoginHandler(playerRepository, securityService, _mapper, _logger);

            // Act:
            var result = await handler.Handle(loginRequest, cancellationToken: default);

            // Assert:
            result.Should().NotBeNull().And.BeOfType<ApiResult<LoginResponse>>();
            result.IsSuccessfulRequest.Should().BeFalse();
            result.Response.Should().BeNull();
            result.ErrorMessages.Should().NotBeNullOrEmpty();
        }
    }
}
