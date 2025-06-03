using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
