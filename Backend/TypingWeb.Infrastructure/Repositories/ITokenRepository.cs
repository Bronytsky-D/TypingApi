using TypingWeb.Domain.Models.Entities;

namespace TypingWeb.Infrastructure.Repositories
{
    public interface ITokenRepository: IRepository<RefreshTokenEntity>
    {
        Task<IEnumerable<RefreshTokenEntity>> GetAllRefreshTokensAsync();
        Task<RefreshTokenEntity> GetRefreshTokenByIdAsync(string id);
        Task AddRefreshTokenAsync(RefreshTokenEntity entity);
        void RemoveRefreshTokenAsync(RefreshTokenEntity entity);
        Task UpdateRefreshTokenAsync(RefreshTokenEntity entity);
    }
}
