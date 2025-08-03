using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TypingWeb.Common;
using TypingWeb.Domain;
using TypingWeb.Domain.Models.Entities;
using TypingWeb.Infrastructure.PostgreSQL.Models;
using TypingWeb.Infrastructure.Repositories;

namespace TypingWeb.Infrastructure.PostgreSQL.Repositories
{
    public class TokenRepository(ApplicationContext ctx, IMapper autoMapper) : ITokenRepository
    {
        private readonly ApplicationContext _context = ctx;
        private readonly IMapper _mapper = autoMapper;

        public async Task<IEnumerable<RefreshTokenEntity>> GetAllAsync()
        {
            var entities = await _context.Set<RefreshToken>()
                .ToListAsync();
            return _mapper.Map<IEnumerable<RefreshTokenEntity>>(entities);
        }

        public async Task<IEnumerable<RefreshTokenEntity>> GetByIdAsync(string id)
        {
            var entity = await _context.Set<RefreshToken>()
                .Where(t => t.Token == id).ToListAsync();
            
            return _mapper.Map<IEnumerable<RefreshTokenEntity>>(entity.FirstOrDefault());
        }

        public async Task<IExecutionResponse> AddAsync(RefreshTokenEntity model)
        {
            var entity = _mapper.Map<RefreshToken>(model);
            await _context.Set<RefreshToken>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return ExecutionResponse.Successful(entity.Id);
        }

        public async Task<IExecutionResponse> UpdateAsync(RefreshTokenEntity model)
        {
            var entity = _mapper.Map<RefreshToken>(model);
            _context.RefreshTokens.Update(entity);
            await _context.SaveChangesAsync();

            return ExecutionResponse.Successful(entity.Id);
        }

        public async Task<IExecutionResponse> RemoveAsync(RefreshTokenEntity model)
        {
            var entity = _mapper.Map<RefreshToken>(model);

            _context.RefreshTokens.Remove(entity);
            _context.SaveChanges();

            return ExecutionResponse.Successful(entity.Id);
        }
    }
}
