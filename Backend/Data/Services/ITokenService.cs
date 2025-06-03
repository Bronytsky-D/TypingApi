using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypingWebApi.Data.Models;

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
