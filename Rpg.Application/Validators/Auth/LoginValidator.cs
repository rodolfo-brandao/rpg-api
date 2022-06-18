using FluentValidation;
using FluentValidation.Results;
using Rpg.Application.Requests.Auth;
using Rpg.Core.Contracts.Repositories;
using Rpg.Core.Contracts.Services;
using Rpg.Core.Models.Nulls;

namespace Rpg.Application.Validators.Auth
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator(IPlayerRepository playerRepository, ISecurityService securityService)
        {
            RuleFor(request => request.Username)
                .NotEmpty()
                .WithMessage(ValidationMessages.ForEmptyProperty);

            RuleFor(request => request.Password)
                .NotEmpty()
                .WithMessage(ValidationMessages.ForEmptyProperty);

            When(request => !string.IsNullOrWhiteSpace(request.Username), () =>
            {
                RuleFor(request => request).CustomAsync(async (request, context, _) =>
                {
                    var player = await playerRepository.GetByUsernameAsync(request.Username, isReadOnly: true);

                    if (player is NullPlayer)
                    {
                        var validationMessage = ValidationMessages.ForRecordNotFound("Player", request.Username);
                        var validationFailure = new ValidationFailure(request.Username, validationMessage);
                        context.AddFailure(validationFailure);
                    }
                    else
                    {
                        var passwordIsValid = securityService.ValidatePassword(request.Password, player.Password, player.PasswordSalt);

                        if (!passwordIsValid)
                        {
                            var validationFailure = new ValidationFailure("Password", "Invalid password.");
                            context.AddFailure(validationFailure);
                        }
                    }
                });
            });
        }
    }
}
