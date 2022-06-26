using Rpg.Tests.Setup.Fakers.Requests.Auth;
using System.Collections;

namespace Rpg.Tests.Setup.Providers.Requests.Auth
{
    internal class LoginRequestWithInvalidData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                LoginRequestFake.WithNoUsername.Generate()
            };

            yield return new object[]
            {
                LoginRequestFake.WithNoPassword.Generate()
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
