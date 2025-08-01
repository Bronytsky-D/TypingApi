using TypingWeb.Domain.Models.Entities;

namespace TypingWeb.Infrastructure.Repositories
{
    public interface IProgressRepository: IRepository<LessonProgressEntity>
    {
        Task<LessonProgressEntity> GetByUserAndLessonAsync(string userId, int lessonId);
        Task<IEnumerable<LessonProgressEntity>> GetByUserAsync(string userId);
    }
}
