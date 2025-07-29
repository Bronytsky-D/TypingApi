using Domain.Models;

namespace Domain.Repositories
{
    public interface IProgressRepository: IRepository<LessonProgress>
    {
        Task<LessonProgress> GetByUserAndLessonAsync(string userId, int lessonId);
        Task<IEnumerable<LessonProgress>> GetByUserAsync(string userId);

    }
}
