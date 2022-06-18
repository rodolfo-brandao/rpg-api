using AutoMapper;
using MediatR;
using Rpg.Application.Requests.Auth;
using Rpg.Application.Responses;
using Rpg.Application.Responses.Auth;
using Rpg.Application.Validators.Auth;
using Rpg.Core.Contracts.Repositories;
using Rpg.Core.Contracts.Services;

namespace Rpg.Application.Handlers.Auth
{
    public class LoginHandler : IRequestHandler<LoginRequest, ApiResult<LoginResponse>>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;

        public LoginHandler(IPlayerRepository playerRepository, ISecurityService securityService, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _securityService = securityService;
            _mapper = mapper;
        }

        public async Task<ApiResult<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetByUsernameAsync(request.Username, isReadOnly: true);

            var validation = await new LoginValidator(player, _securityService).ValidateAsync(request, cancellationToken);

            if (!validation.IsValid)
            {
                return new ApiResult<LoginResponse>(validation.Errors);
            }

            var response = _mapper.Map<LoginResponse>(player);
            response.AccessToken = _securityService.CreatePlayerAccessToken(player);

            return new ApiResult<LoginResponse>(response);
        }
    }
}
