using TypingWeb.Common;
using TypingWeb.Domain.Models.Entities;

namespace TypingWeb.Infrastructure.Repositories
{
    public interface ITokenRepository: IRepository<RefreshTokenEntity>
    {
        Task<IEnumerable<RefreshTokenEntity>> GetByIdAsync(string id);
        Task<IExecutionResponse> AddAsync(RefreshTokenEntity model);

    }
}
