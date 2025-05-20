using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TypingWebApi.Data.Models;

namespace Domain.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<User> GetUserById(string id);
        public Task<User> GetUserByEmail(string email);
        public Task<IdentityResult> CreateUser(User newUser, string password);
        Task<IdentityResult> CreateUser(User user);

        public Task<bool> CheckUserPassword(User user, string password);
        public Task UpdateUser(User userToBeUpdated, User user);
        public Task DeleteUser(User user);

        Task<bool> AddExperienceAsync(string userId, int xp);
        Task<List<User>> GetLeaderboardAsync(int count = 10);
    }
}
