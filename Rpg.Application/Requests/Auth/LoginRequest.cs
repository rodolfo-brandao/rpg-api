using MediatR;
using Rpg.Application.Responses;
using Rpg.Application.Responses.Auth;

namespace Rpg.Application.Requests.Auth
{
    public class LoginRequest : IRequest<ApiResult<LoginResponse>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
