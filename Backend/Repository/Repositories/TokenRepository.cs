using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypingWebApi.Data;
using TypingWebApi.Data.Context;

namespace Repository.Repositories
{
    public class TokenRepository : Repository<RefreshToken>, ITokenRepository
    {
        private readonly ApplicationContext _context;

        public TokenRepository(ApplicationContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RefreshToken>> GetAllRefreshTokensAsync()
        {
            return await _context.RefreshTokens.ToListAsync();
        }

        public async Task<RefreshToken> GetRefreshTokenByIdAsync(string id)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(t => t.Token == id);
        }

        public async Task AddRefreshTokenAsync(RefreshToken entity)
        {
            await _context.RefreshTokens.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRefreshTokenAsync(RefreshToken entity)
        {
            _context.RefreshTokens.Update(entity);
            await _context.SaveChangesAsync();
        }

        public void RemoveRefreshTokenAsync(RefreshToken entity)
        {
            _context.RefreshTokens.Remove(entity);
            _context.SaveChanges();
        }
        private ApplicationContext Context => (ApplicationContext)_context;
    }
}
