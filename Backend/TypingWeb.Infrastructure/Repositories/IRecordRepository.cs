using TypingWeb.Common;
using TypingWeb.Domain.Models.Entities;

namespace TypingWeb.Infrastructure.Repositories
{
    public interface IRecordRepository: IRepository<RecordEntity>
    {
        Task<IEnumerable<RecordEntity>> GetByUserAsync(string userId);
        Task<IExecutionResponse> AddAsync(RecordEntity model);
    }
}
