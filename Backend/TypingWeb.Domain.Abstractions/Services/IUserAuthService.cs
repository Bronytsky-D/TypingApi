

using TypingWeb.Domain.Models.Entities;

namespace TypingWeb.Domain.Abstractions.Services
{
    public interface IUserAuthService
    {
        public Task<bool> CheckUserPassword(UserProfileEntity user, string password);
    }
}
