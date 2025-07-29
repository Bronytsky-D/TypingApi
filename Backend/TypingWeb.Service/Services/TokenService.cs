using Domain.Models;
using Domain.Models.Types;
using Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Service.Services
{
    public class TokenService: ITokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public TokenService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public string GenerateAccessToken(User user)
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

        public async Task SaveRefreshTokenAsync(User user, string token)
        {
            var refresh = new RefreshToken
            {
                Token = token,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
            };

            await _unitOfWork.RefreshToken.AddRefreshTokenAsync(refresh);
            await _unitOfWork.CommitAsync();
        }

        public async Task<RefreshToken?> GetValidRefreshTokenAsync(string userId, string refreshToken)
        {
            var token = await _unitOfWork.RefreshToken.GetRefreshTokenByIdAsync(refreshToken);

            if (token != null &&
                token.UserId == userId &&
                !token.IsRevoked &&
                token.ExpiresAt > DateTime.UtcNow)
            {
                return token;
            }

            return null;
        }
    }
}
