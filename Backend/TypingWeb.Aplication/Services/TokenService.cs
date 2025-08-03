using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TypingWeb.Domain.Models.Entities;
using TypingWeb.Domain.Abstractions.Services;
using TypingWeb.Infrastructure;

namespace TypingWeb.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public TokenService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public string GenerateAccessToken(UserEntity user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSetting:SecurityKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new Claim(ClaimTypes.Role, "User")
            };

            var token = new JwtSecurityToken(
                issuer: _config["JWTSetting:validIssuer"],
                audience: _config["JWTSetting:validAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        public async Task SaveRefreshTokenAsync(UserEntity user, string token)
        {
            var refresh = new RefreshTokenEntity
            {
                Token = token,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
            };

            await _unitOfWork.RefreshToken.AddAsync(refresh);
            await _unitOfWork.CommitAsync();
        }

        public async Task<RefreshTokenEntity?> GetValidRefreshTokenAsync(string userId, string refreshToken)
        {
            var allTokens = await _unitOfWork.RefreshToken.GetByIdAsync(userId);
            var validToken = allTokens
                  .SingleOrDefault(t =>
                      t.Token == refreshToken &&
                      !t.IsRevoked &&
                      t.ExpiresAt > DateTime.UtcNow);
            var invalidTokens = allTokens
                  .Where(t => t.Token != refreshToken || t.IsRevoked || t.ExpiresAt <= DateTime.UtcNow);

            foreach (var bad in invalidTokens)
            {
                await _unitOfWork.RefreshToken.RemoveAsync(bad);
            }


            return validToken;
        }
    }
}
