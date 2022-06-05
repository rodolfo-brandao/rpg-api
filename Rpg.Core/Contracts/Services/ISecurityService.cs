using Rpg.Core.Models;
using System.Security.Claims;

namespace Rpg.Core.Contracts.Services
{
    public interface ISecurityService
    {
        (string PasswordHash, string PasswordSalt) CreatePasswordHash(string rawPassword);
        string CreatePlayerAccessToken(Player player);
        Task<Player> GetAuthenticatedPlayer(ClaimsPrincipal player);
        bool ValidatePassword(string rawPassword, string passwordHash, string passwordSalt);
    }
}
