using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypingWebApi.Data.Models;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        public ValueTask<User> GetUserByIdAsync(string id);
        public ValueTask<User> GetUserByEmailAsync(string email);
        public Task<IEnumerable<User>> GetAllUserAsync();
        public Task<bool> CheckUserPasswordAsync(User user, string password);
        public Task<IdentityResult> AddUserAsync(User entity, string password);
        Task<IdentityResult> AddUserAsync(User user);

        public Task RemoveUserAsync(User entity);

        Task UpdateAsync(User user);

        Task AddToRoleAsync(User user, string role);
    }
}
