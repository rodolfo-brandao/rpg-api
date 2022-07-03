using AutoMapper;
using MediatR;
using Rpg.Application.Requests.Auth;
using Rpg.Application.Responses;
using Rpg.Application.Responses.Auth;
using Rpg.Application.Validators.Auth;
using Rpg.Core.Contracts.Repositories;
using Rpg.Core.Contracts.Services;
using Serilog;

namespace Rpg.Application.Handlers.Auth
{
    public class LoginHandler : IRequestHandler<LoginRequest, ApiResult<LoginResponse>>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public LoginHandler(IPlayerRepository playerRepository, ISecurityService securityService, IMapper mapper, ILogger logger)
        {
            _playerRepository = playerRepository;
            _securityService = securityService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResult<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            _logger.Information($"Performing log in to Player with username '{request.Username}'.");

            var player = await _playerRepository.GetByUsernameAsync(request.Username, isReadOnly: true);

            var validation = await new LoginValidator(player, _securityService).ValidateAsync(request, cancellationToken);

            if (!validation.IsValid)
            {
                _logger.Error($"Validation failed when trying to log in to Player with username '{request.Username}'.");
                return new ApiResult<LoginResponse>(validation.Errors);
            }

            var response = _mapper.Map<LoginResponse>(player);
            response.AccessToken = _securityService.CreatePlayerAccessToken(player);

            _logger.Information($"Player with username '{request.Username}' logged in successfuly.");
            return new ApiResult<LoginResponse>(response);
        }
    }
}
