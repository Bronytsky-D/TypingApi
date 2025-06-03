using Domain;
using Domain.Repositories;
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
        private readonly IUnitOfWork _unitOfWork;

        public RecordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IExecutionResponse> GetRecordsByUserIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return ExecutionResponse.Failure("userId is null or empty");
            }

            var records = await _unitOfWork.Record.GetRecordsByUserIdAsync(userId);
            return ExecutionResponse.Successful(records);
        }

        public async Task<IExecutionResponse> AddRecordAsync(Record record)
        {
            if (string.IsNullOrWhiteSpace(record.UserId))
            {
                return ExecutionResponse.Failure("userId is null or empty");
            }

            await _unitOfWork.Record.AddRecordAsync(record);
            await _unitOfWork.CommitAsync();

            return ExecutionResponse.Successful(record);
        }
    }
}
