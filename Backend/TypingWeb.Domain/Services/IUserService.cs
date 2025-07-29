using Domain.Models;
using Domain.Models.Types;

namespace Domain.Services
{
    public interface IUserService
    {
        public Task<IExecutionResponse> GetAllUsers();
        public Task<IExecutionResponse> GetUserById(string id);
        public Task<IExecutionResponse> GetUserByEmail(string email);
        public Task<IExecutionResponse> CreateUser(User newUser, string password);
        Task<IExecutionResponse> CreateUser(User user);

        public Task<bool> CheckUserPassword(User user, string password);
        public Task UpdateUser(User userToBeUpdated, User user);
        public Task DeleteUser(User user);
        Task<bool> AddExperienceAsync(string userId, int xp);

    }
}
