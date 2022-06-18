using Rpg.Application.Requests.Auth;

namespace Rpg.Tests.Setup.Fakers.Requests.Auth
{
    internal static class LoginRequestFake
    {
        #region Valid
        public static Faker<LoginRequest> Valid(string username = default, string password = default) => new Faker<LoginRequest>()
            .RuleFor(request => request.Username, setter => username ?? setter.Internet.UserName())
            .RuleFor(request => request.Password, setter => password ?? setter.Internet.Password()); 
        #endregion

        #region Invalid
        public static Faker<LoginRequest> WithNoUsername => new Faker<LoginRequest>()
            .RuleFor(request => request.Username, _ => default)
            .RuleFor(request => request.Password, setter => setter.Internet.Password());

        public static Faker<LoginRequest> WithNoPassword => new Faker<LoginRequest>()
            .RuleFor(request => request.Username, setter => setter.Internet.UserName())
            .RuleFor(request => request.Password, _ => default); 
        #endregion

    }
}
