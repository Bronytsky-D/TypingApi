using Microsoft.AspNetCore.Identity;
using TypingWeb.Domain.Models.Entities;
using TypingWeb.Domain.Abstractions.Services;

namespace TypingWeb.Service.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly UserManager<UserEntity> _userManager;
        public UserAuthService(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> CheckUserPassword(UserEntity user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
