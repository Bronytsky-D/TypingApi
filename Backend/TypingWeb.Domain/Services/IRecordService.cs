using Domain.Models;
using Domain.Models.Types;

namespace TypingWebApi.Service
{
    public interface IRecordService 
    {
        Task<IExecutionResponse> GetRecordsByUserIdAsync(string userd);
        Task<IExecutionResponse> AddRecordAsync(Record record);

    }
}
