using Domain.Models;
using Domain.Models.Types;

namespace Domain.Services
{
    public interface IProgressService
    {
        Task<IExecutionResponse> GetByUserAndLessonIdAsync(string userId, int lessonId);
        Task<IExecutionResponse> GetByUserIdAsync(string userId);
        Task<IExecutionResponse> UpsertAsync(LessonProgress entity);
    }
}
