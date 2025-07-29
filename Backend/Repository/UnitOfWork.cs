using Domain;
using Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories;
using TypingWebApi.Data.Context;
using TypingWebApi.Data.Models;

namespace Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private RecordRepository _recordRepository;
        private TokenRepository _refreshTokenRepository;
        private ProgressRepository _progrssRepository;


        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }
        public IRecordRepository Record => _recordRepository ??= new RecordRepository(_context);
        public ITokenRepository RefreshToken => _refreshTokenRepository ??= new TokenRepository(_context);
        public IProgressRepository Progress => _progrssRepository ??= new ProgressRepository(_context);
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
