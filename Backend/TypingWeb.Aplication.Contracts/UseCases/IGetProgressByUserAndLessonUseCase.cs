using TypingWeb.Common;

namespace TypingWeb.Aplication.Abstractions.UseCases
{
    public interface IGetProgressByUserAndLessonUseCase
    {
        Task<IExecutionResponse> ExecuteAsync(string userId, int lessonId);
    }
}
