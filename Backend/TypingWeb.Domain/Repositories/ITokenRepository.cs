using Domain.Models;

namespace Domain.Repositories
{
    public interface ITokenRepository: IRepository<RefreshToken>
    {
        Task<IEnumerable<RefreshToken>> GetAllRefreshTokensAsync();
        Task<RefreshToken> GetRefreshTokenByIdAsync(string id);
        Task AddRefreshTokenAsync(RefreshToken entity);
        void RemoveRefreshTokenAsync(RefreshToken entity);
        Task UpdateRefreshTokenAsync(RefreshToken entity);
    }
}
