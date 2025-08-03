using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TypingWeb.Domain.Models.Entities;
using TypingWeb.Domain.Abstractions.Services;
using TypingWeb.Infrastructure.PostgreSQL.Models;
using AutoMapper;

namespace TypingWeb.Service.Services
{
    public  class UserGameService : IUserGameService
    {
        private readonly UserManager<User> _userManager;
        public UserGameService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> AddExperienceAsync(string userId, int xp)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return false;
            while (user.ExperiencePoints >= 100 * (user.Level + 1))
            {
                user.Level++;

                user.Achievements ??= new List<string>();
                user.Achievements.Add($"Level {user.Level} reached");
            }
            await _userManager.UpdateAsync(user);

            return true;
        }

    }
}
