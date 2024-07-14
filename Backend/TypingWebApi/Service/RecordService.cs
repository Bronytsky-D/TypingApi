using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TypingWebApi.Data;
using TypingWebApi.Data.Context;
using TypingWebApi.Data.Models;
using TypingWebApi.Domains.Models.Types;

namespace TypingWebApi.Service
{
    public class RecordService : IRecordService
    {
        private readonly IRepository<Record> _repository;

        public RecordService(IRepository<Record> repository)
        {
            _repository=repository;
        }

        public async Task<IExecutionResponse> GetRecordsByUserIdAsync(string userId)
        {
            if(userId == string.Empty)
            {
                return ExecutionResponse.Failure("userId is null");
            }

            var records = await _repository.FindAsync(r => r.UserId == userId);
            return ExecutionResponse.Successful(records);
                
        }

        public async Task<IExecutionResponse> AddRecordAsync(Record record)
        {
            if (record.UserId == string.Empty)
            {
                return ExecutionResponse.Failure("userId is null");
            }
            await _repository.AddAsync(record);
            return ExecutionResponse.Successful(record);
        }

    }
}
