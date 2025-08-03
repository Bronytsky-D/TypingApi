using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TypingWeb.Common;
using TypingWeb.Domain;
using TypingWeb.Domain.Models.Entities;
using TypingWeb.Infrastructure.PostgreSQL.Models;
using TypingWeb.Infrastructure.Repositories;


namespace TypingWeb.Infrastructure.PostgreSQL.Repositories
{
    public class RecordRepository(ApplicationContext ctx, IMapper autoMapper): IRecordRepository
    {
        private readonly ApplicationContext _context = ctx;
        private readonly IMapper _mapper = autoMapper;

        public async Task<IEnumerable<RecordEntity>> GetByUserAsync(string userId)
        {
            var entites = await _context.Set<Record>()
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<RecordEntity>>(entites);
        }

        public async Task<IEnumerable<RecordEntity>> GetAllAsync()
        {
            var entites = await _context.Set<Record>().ToListAsync();

            return _mapper.Map<IEnumerable<RecordEntity>>(entites);
        }
        public async Task<IExecutionResponse> AddAsync(RecordEntity model)
        {
           var entite = _mapper.Map<Record>(model);
           await _context.Set<Record>().AddAsync(entite);
           await _context.SaveChangesAsync();

           return ExecutionResponse.Successful(entite.Id);
        }
        public async Task<IExecutionResponse> RemoveAsync(RecordEntity model)
        {
            var record = _mapper.Map<Record>(model);
            _context.Set<Record>().Remove(record);
            await _context.SaveChangesAsync();

            return ExecutionResponse.Successful(record.Id);
        }
        public async Task<IExecutionResponse> UpdateAsync(RecordEntity model)
        {
            var record = _mapper.Map<Record>(model);
            _context.Set<Record>().Update(record);
            await _context.SaveChangesAsync();
            return ExecutionResponse.Successful(record.Id);
        }

    }
}
