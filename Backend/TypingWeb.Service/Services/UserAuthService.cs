using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Identity;

namespace Service.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly UserManager<User> _userManager;
        public UserAuthService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> CheckUserPassword(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
