using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TypingWebApi.Data.Models;
using Domain.Repositories;

namespace Repository.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly UserManager<User> _userManager;
        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async ValueTask<User> GetUserByIdAsync(string id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async ValueTask<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.Users.FirstOrDefaultAsync(m => m.Email == email);
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
        public async Task<bool> CheckUserPasswordAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> AddUserAsync(User entity, string password)
        {
            return await _userManager.CreateAsync(entity, password);
        }
        public async Task<IdentityResult> AddUserAsync(User user)
        {
            return await _userManager.CreateAsync(user); // без пароля
        }

        public async Task RemoveUserAsync(User entity)
        {
            await _userManager.DeleteAsync(entity);
        }

        public async Task UpdateAsync(User user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task AddToRoleAsync(User user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }
        public async Task AddExperienceAsync(User user, int xpToAdd)
        {
            user.ExperiencePoints += xpToAdd;

            while (user.ExperiencePoints >= 100*(user.Level + 1))
            {
                user.Level++;

                // Приклад: додаємо досягнення за кожен рівень
                user.Achievements ??= new List<string>();
                user.Achievements.Add($"Level {user.Level} reached");
            }

            await _userManager.UpdateAsync(user);
        }

    }
}
