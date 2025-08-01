using TypingWeb.Common;
using TypingWeb.Domain.Models.Entities;

namespace TypingWeb.Domain.Abstractions.Services
{
    public interface IProgressService
    {
        Task<IExecutionResponse> GetByUserAndLessonIdAsync(string userId, int lessonId);
        Task<IExecutionResponse> GetByUserIdAsync(string userId);
        Task<IExecutionResponse> UpsertAsync(LessonProgressEntity entity);
    }
}
