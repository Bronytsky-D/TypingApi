using TypingWeb.Aplication.Abstractions.UseCases;
using TypingWeb.Common;
using TypingWeb.Domain;
using TypingWeb.Domain.Abstractions.Services;

namespace TypingWeb.Aplication.UseCases
{
    public class GetProgressByUserAndLessonUseCase : IGetProgressByUserAndLessonUseCase
    {
        private readonly IProgressService _progressService;
        public GetProgressByUserAndLessonUseCase(IProgressService progressService)
        {
            _progressService = progressService;
        }
        public async Task<IExecutionResponse> ExecuteAsync(string userId, int lessonId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return ExecutionResponse.Failure("User ID and Lesson ID cannot be null or empty.");
            }
            if (lessonId < 0)
            {
                return ExecutionResponse.Failure("Lesson ID must be a positive integer.");
            }
            var response = await _progressService.GetByUserAndLessonIdAsync(userId, lessonId);
            if (!response.Success)
            {
                return ExecutionResponse.Failure(response.Errors);
            }
            return response;
        }
    }
}
