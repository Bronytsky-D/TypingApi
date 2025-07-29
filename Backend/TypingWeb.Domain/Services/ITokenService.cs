using Domain.Models;

namespace Domain.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        Task SaveRefreshTokenAsync(User user, string token);
        Task<RefreshToken?> GetValidRefreshTokenAsync(string userId, string refreshToken);
    }
}
