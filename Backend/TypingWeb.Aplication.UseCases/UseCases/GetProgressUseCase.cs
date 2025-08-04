
using TypingWeb.Aplication.Abstractions.UseCases;
using TypingWeb.Common;
using TypingWeb.Domain;
using TypingWeb.Domain.Abstractions.Services;

namespace TypingWeb.Aplication.UseCases
{
    public class GetProgressUseCase : IGetProgressUseCase
    {
        private readonly IProgressService _progressService;
        public GetProgressUseCase(IProgressService progressService)
        {
            _progressService = progressService;
        }
        public async Task<IExecutionResponse> ExecuteAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return ExecutionResponse.Failure("User ID cannot be null or empty.");
            }
            var response = await _progressService.GetByUserIdAsync(userId);
            if (!response.Success)
            {
                return ExecutionResponse.Failure(response.Errors);
            }
            return response;
        }
    }
}
