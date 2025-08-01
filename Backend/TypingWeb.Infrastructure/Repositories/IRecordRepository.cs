using TypingWeb.Domain.Models.Entities;

namespace TypingWeb.Infrastructure.Repositories
{
    public interface IRecordRepository: IRepository<RecordEntity>
    {
        Task<IEnumerable<RecordEntity>> GetRecordsByUserIdAsync(string userId);
        public Task<IEnumerable<RecordEntity>> GetAllRecordAsync();
        public Task AddRecordAsync(RecordEntity entity);
        public void RemoveRecordAsync(RecordEntity entity);
    }
}
