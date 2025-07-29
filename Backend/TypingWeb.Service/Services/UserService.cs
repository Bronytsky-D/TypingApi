using Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Models.Types;
using Repository.ExecutionResponse;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IExecutionResponse> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return ExecutionResponse.Successful(users);
        }

        public async Task<IExecutionResponse> GetUserById(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return ExecutionResponse.Failure("User not found");
            }
            return ExecutionResponse.Successful(user);
        }

        public async Task<IExecutionResponse> GetUserByEmail(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return ExecutionResponse.Failure("User not found");
            }
            return ExecutionResponse.Successful(user);
        }

        public async Task<IExecutionResponse> CreateUser(User newUser, string password)
        {
            var user = await _userManager.CreateAsync(newUser, password);
            if (user.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "User");
                return ExecutionResponse.Successful(newUser);
            }
            return ExecutionResponse.Failure("User not created");
        }
        public async Task<IExecutionResponse> CreateUser(User newUser)
        {
            var user = await _userManager.CreateAsync(newUser);
            if (user.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "User");
                return ExecutionResponse.Successful(newUser);
            }
            return ExecutionResponse.Failure("User not created");
        }

        public async Task<bool> CheckUserPassword(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task UpdateUser(User userToBeUpdated, User userData)
        {
            userToBeUpdated.FullName = userData.FullName;
            userToBeUpdated.Email = userData.Email;

            await _userManager.UpdateAsync(userToBeUpdated);
        }

        public async Task DeleteUser(User user)
        {
            await _userManager.DeleteAsync(user);
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