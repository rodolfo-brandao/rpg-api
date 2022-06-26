using Rpg.Core.Enums;
using Rpg.Core.Models;

namespace Rpg.Tests.Setup.Fakers.Models
{
    internal static class PlayerFake
    {
        private static IEnumerable<Role> RoleNames => new List<Role>
        {
            Role.Admin,
            Role.User
        };

        #region Valid
        public static Faker<Player> Valid(string username = default) => new Faker<Player>()
            .RuleFor(player => player.Id, Guid.NewGuid())
            .RuleFor(player => player.Username, setter => username ?? setter.Internet.UserName())
            .RuleFor(player => player.Email, setter => setter.Person.Email)
            .RuleFor(player => player.Password, setter => setter.Internet.Password())
            .RuleFor(player => player.PasswordSalt, setter => setter.Hashids.Encode())
            .RuleFor(player => player.Role, setter => setter.PickRandom(RoleNames))
            .RuleFor(player => player.CreatedAt, DateTime.UtcNow)
            .RuleFor(player => player.UpdatedAt, _ => default)
            .RuleFor(player => player.IsDisabled, _ => default);
        #endregion

        #region Invalid
        public static Faker<Player> WithNoUsername => new Faker<Player>()
            .RuleFor(player => player.Id, Guid.NewGuid())
            .RuleFor(player => player.Username, _ => default)
            .RuleFor(player => player.Email, setter => setter.Person.Email)
            .RuleFor(player => player.Password, setter => setter.Internet.Password())
            .RuleFor(player => player.PasswordSalt, setter => setter.Hashids.Encode())
            .RuleFor(player => player.Role, setter => setter.PickRandom(RoleNames))
            .RuleFor(player => player.CreatedAt, DateTime.UtcNow)
            .RuleFor(player => player.UpdatedAt, _ => default)
            .RuleFor(player => player.IsDisabled, _ => default);

        public static Faker<Player> WithNoEmail => new Faker<Player>()
            .RuleFor(player => player.Id, Guid.NewGuid())
            .RuleFor(player => player.Username, setter => setter.Internet.UserName())
            .RuleFor(player => player.Email, _ => default)
            .RuleFor(player => player.Password, setter => setter.Internet.Password())
            .RuleFor(player => player.PasswordSalt, setter => setter.Hashids.Encode())
            .RuleFor(player => player.Role, setter => setter.PickRandom(RoleNames))
            .RuleFor(player => player.CreatedAt, DateTime.UtcNow)
            .RuleFor(player => player.UpdatedAt, _ => default)
            .RuleFor(player => player.IsDisabled, _ => default);

        public static Faker<Player> WithNoPassword => new Faker<Player>()
            .RuleFor(player => player.Id, Guid.NewGuid())
            .RuleFor(player => player.Username, setter => setter.Internet.UserName())
            .RuleFor(player => player.Email, setter => setter.Person.Email)
            .RuleFor(player => player.Password, _ => default)
            .RuleFor(player => player.PasswordSalt, _ => default)
            .RuleFor(player => player.Role, setter => setter.PickRandom(RoleNames))
            .RuleFor(player => player.CreatedAt, DateTime.UtcNow)
            .RuleFor(player => player.UpdatedAt, _ => default)
            .RuleFor(player => player.IsDisabled, _ => default); 
        #endregion
    }
}
