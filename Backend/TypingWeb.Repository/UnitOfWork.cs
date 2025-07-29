using Domain.Models.Types;
using Domain.Repositories;
using Repository.Context;
using Repository.Repositories;


namespace Repository.UnitOfWork
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
