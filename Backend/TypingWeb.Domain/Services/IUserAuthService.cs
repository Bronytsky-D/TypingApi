using Domain.Models;

namespace Domain.Services
{
    public interface IUserAuthService
    {
        public Task<bool> CheckUserPassword(User user, string password);
    }
}
