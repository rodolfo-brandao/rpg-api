using FluentValidation;
using Rpg.Application.Validators.Auth;
using Rpg.Tests.Setup.Builders.Services;
using Rpg.Tests.Setup.Fakers.Models;
using Rpg.Tests.Setup.Fakers.Requests.Auth;

namespace Rpg.Tests.Application.Validators.Auth
{
    [Trait("Application", "Validator")]
    public class LoginValidatorTest
    {
        [Fact(DisplayName = $"{nameof(LoginValidator)} - Success")]
        public async Task ValidateRequestObject_PassValidRequestObject_ValidationRulesShouldSucceed()
        {
            // Arrange:
            var request = LoginRequestFake
                .Valid()
                .Generate();

            var player = PlayerFake
                .Valid
                .Generate();

            var securityService = SecurityServiceMockBuilder
                .Create()
                .SetupValidatePassword(passwordIsValid: true)
                .Build();

            var validator = new LoginValidator(player, securityService)
            {
                ClassLevelCascadeMode = CascadeMode.Stop
            };

            // Act:
            var validation = await validator.ValidateAsync(request);

            // Assert:
            validation.Should().NotBeNull().And.BeOfType<FluentValidation.Results.ValidationResult>();
            validation.IsValid.Should().BeTrue();
            validation.Errors.Should().BeEmpty();
        }

        [Fact(DisplayName = $"{nameof(LoginValidator)} - Fail: no username")]
        public async Task ValidateRequestObject_PassRequestObjectWithNoUsername_ValidationRulesShouldFail()
        {
            // Arrange:
            var request = LoginRequestFake
                .WithNoUsername
                .Generate();

            var player = PlayerFake
                .Valid
                .Generate();

            var securityService = SecurityServiceMockBuilder
                .Create()
                .SetupValidatePassword(passwordIsValid: true)
                .Build();

            var validator = new LoginValidator(player, securityService)
            {
                ClassLevelCascadeMode = CascadeMode.Stop
            };

            // Act:
            var validation = await validator.ValidateAsync(request);

            // Assert:
            validation.Should().NotBeNull().And.BeOfType<FluentValidation.Results.ValidationResult>();
            validation.IsValid.Should().BeFalse();
            validation.Errors.Should().NotBeEmpty().And.HaveCount(1);
        }

        [Fact(DisplayName = $"{nameof(LoginValidator)} - Fail: no password")]
        public async Task ValidateRequestObject_PassRequestObjectWithNoPassword_ValidationRulesShouldFail()
        {
            // Arrange:
            var request = LoginRequestFake
                .WithNoPassword
                .Generate();

            var player = PlayerFake
                .Valid
                .Generate();

            var securityService = SecurityServiceMockBuilder
                .Create()
                .SetupValidatePassword(passwordIsValid: true)
                .Build();

            var validator = new LoginValidator(player, securityService)
            {
                ClassLevelCascadeMode = CascadeMode.Stop
            };

            // Act:
            var validation = await validator.ValidateAsync(request);

            // Assert:
            validation.Should().NotBeNull().And.BeOfType<FluentValidation.Results.ValidationResult>();
            validation.IsValid.Should().BeFalse();
            validation.Errors.Should().NotBeEmpty().And.HaveCount(1);
        }

        [Fact(DisplayName = $"{nameof(LoginValidator)} - Fail: wrong password")]
        public async Task ValidateRequestObject_PassRequestObjectWithWrongPassword_ValidationRulesShouldFail()
        {
            // Arrange:
            var request = LoginRequestFake
                .Valid()
                .Generate();

            var player = PlayerFake
                .Valid
                .Generate();

            var securityService = SecurityServiceMockBuilder
                .Create()
                .SetupValidatePassword()
                .Build();

            var validator = new LoginValidator(player, securityService)
            {
                ClassLevelCascadeMode = CascadeMode.Stop
            };

            // Act:
            var validation = await validator.ValidateAsync(request);

            // Assert:
            validation.Should().NotBeNull().And.BeOfType<FluentValidation.Results.ValidationResult>();
            validation.IsValid.Should().BeFalse();
            validation.Errors.Should().NotBeEmpty().And.HaveCount(1);
        }
    }
}
