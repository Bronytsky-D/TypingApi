using Domain.Models;

namespace Domain.Repositories
{
    public interface IRecordRepository: IRepository<Record>
    {
        Task<IEnumerable<Record>> GetRecordsByUserIdAsync(string userId);
        public Task<IEnumerable<Record>> GetAllRecordAsync();
        public Task AddRecordAsync(Record entity);
        public void RemoveRecordAsync(Record entity);
    }
}
