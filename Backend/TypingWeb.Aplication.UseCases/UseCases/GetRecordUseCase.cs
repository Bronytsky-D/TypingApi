using TypingWeb.Aplication.Abstractions.UseCases;
using TypingWeb.Common;
using TypingWeb.Domain;
using TypingWeb.Domain.Abstractions.Services;

namespace TypingWeb.Aplication.UseCases
{
    public class GetRecordUseCase : IGetRecordUseCase
    {
        private readonly IRecordService _recordService;
        public GetRecordUseCase(IRecordService recordService)
        {
            _recordService = recordService;
        }
        public async Task<IExecutionResponse> ExecuteAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return ExecutionResponse.Failure("User ID cannot be null or empty.");
            }
            var response = await _recordService.GetRecordsByUserIdAsync(userId);
            if (!response.Success)
            {
                return ExecutionResponse.Failure(response.Errors);
            }
            return response;
        }

    }
}
