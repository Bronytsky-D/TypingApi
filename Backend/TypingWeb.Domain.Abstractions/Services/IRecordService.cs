
using TypingWeb.Common;
using TypingWeb.Domain.Models.Entities;

namespace TypingWeb.Domain.Abstractions.Services
{
    public interface IRecordService 
    {
        Task<IExecutionResponse> GetRecordsByUserIdAsync(string userd);
        Task<IExecutionResponse> AddRecordAsync(RecordEntity record);
    }
}
