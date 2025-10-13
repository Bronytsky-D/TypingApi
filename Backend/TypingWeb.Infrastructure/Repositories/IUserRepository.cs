using TypingWeb.Common;
using TypingWeb.Domain.Models.Entities;

namespace TypingWeb.Infrastructure.Repositories
{
    public interface IUserRepository : IRepository<UserProfileEntity>
    {
        Task<IExecutionResponse?> GetByUserAsync(string id);
    }
}
