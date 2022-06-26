using FluentValidation;
using Rpg.Application.Requests.Auth;
using Rpg.Core.Contracts.Services;
using Rpg.Core.Models;
using Rpg.Core.Models.Nulls;

namespace Rpg.Application.Validators.Auth
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator(Player player, ISecurityService securityService)
        {
            RuleFor(request => request.Username)
                .NotEmpty()
                .WithMessage(request => ValidationMessages.ForEmptyProperty(nameof(request.Username)));

            RuleFor(request => request.Password)
                .NotEmpty()
                .WithMessage(request => ValidationMessages.ForEmptyProperty(nameof(request.Password)));

            RuleFor(request => request)
                .Must((request) => player is not NullPlayer)
                .WithMessage(request => ValidationMessages.ForRecordNotFound("Player", request.Username))
                .Must((request) => securityService.ValidatePassword(request.Password, player.Password, player.PasswordSalt))
                .WithMessage("Invalid password.");
        }
    }
}
