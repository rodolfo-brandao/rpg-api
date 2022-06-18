using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rpg.Application.Extensions;
using Rpg.Core.Contracts.Repositories;
using Rpg.Core.Contracts.Services;
using Rpg.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Rpg.Application.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IConfiguration _configuration;
        private readonly IPlayerRepository _playerRepository;

        public SecurityService(IConfiguration configuration, IPlayerRepository playerRepository)
        {
            _configuration = configuration;
            _playerRepository = playerRepository;
        }

        #region Public Methods
        public (string PasswordHash, string PasswordSalt) CreatePasswordHash(string rawPassword)
        {
            var salt = CreateSalt();
            return (CreateHash(rawPassword, salt), salt);
        }

        public string CreatePlayerAccessToken(Player player)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Secret").Value));
            var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, player.Id.ToString()),
                new Claim(ClaimTypes.Role, player.Role.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = signInCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public async Task<Player> GetAuthenticatedPlayer(ClaimsPrincipal player)
        {
            _ = Guid.TryParse(player.FindFirst(ClaimTypes.Name)?.Value, out var playerId);
            return await _playerRepository.GetByIdAsync(playerId);
        }

        public bool ValidatePassword(string rawPassword, string passwordHash, string passwordSalt)
        {
            return CreateHash(rawPassword, passwordSalt).Equals(passwordHash);
        }
        #endregion

        #region Private Methods
        private static string CreateHash(string rawPassword, string salt)
        {
            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes($"{rawPassword}{salt}"));
            return hash.ParseToString("x2");
        }

        private static string CreateSalt(int size = 12)
        {
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            var bytes = new byte[size];
            randomNumberGenerator.GetBytes(bytes);
            return bytes.ParseToString("x2");
        }
        #endregion
    }
}
