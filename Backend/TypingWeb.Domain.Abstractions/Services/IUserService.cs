using TypingWeb.Common;
using TypingWeb.Domain.Models.Entities;

namespace TypingWeb.Domain.Abstractions.Services
{
    public interface IUserService
    {
        public Task<IExecutionResponse> GetAllUsers();
        public Task<IExecutionResponse> GetUserById(string id);
        public Task<IExecutionResponse> GetUserByEmail(string email);
        public Task<IExecutionResponse> CreateUser(UserEntity model, string? password = null);
        public Task<IExecutionResponse> DeleteUser(UserEntity model);
    }
}
