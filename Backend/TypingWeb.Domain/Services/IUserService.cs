using Domain.Models;
using Domain.Models.Types;

namespace Domain.Services
{
    public interface IUserService
    {
        public Task<IExecutionResponse> GetAllUsers();
        public Task<IExecutionResponse> GetUserById(string id);
        public Task<IExecutionResponse> GetUserByEmail(string email);
        public Task<IExecutionResponse> CreateUser(User newUser, string ?password = null);
        public Task<IExecutionResponse> DeleteUser(User user);

    }
}
