using AutoMapper;
using TypingWeb.Infrastructure.PostgreSQL.Repositories;
using TypingWeb.Infrastructure.Repositories;


namespace TypingWeb.Infrastructure.PostgreSQL
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private RecordRepository _recordRepository;
        private TokenRepository _refreshTokenRepository;
        private ProgressRepository _progrssRepository;


        public UnitOfWork(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IRecordRepository Record => _recordRepository ??= new RecordRepository(_context, _mapper);
        public ITokenRepository RefreshToken => _refreshTokenRepository ??= new TokenRepository(_context, _mapper);

        public IProgressRepository Progress => _progrssRepository ??= new ProgressRepository(_context, _mapper);

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
