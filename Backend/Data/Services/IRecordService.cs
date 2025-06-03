using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TypingWebApi.Data.Models;
using TypingWebApi.Domains.Models.Types;

namespace TypingWebApi.Service
{
    public interface IRecordService 
    {
        Task<IExecutionResponse> GetRecordsByUserIdAsync(string userd);
        Task<IExecutionResponse> AddRecordAsync(Record record);

    }
}
