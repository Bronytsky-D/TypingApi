using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IProgressRepository: IRepository<LessonProgress>
    {
        Task<LessonProgress> GetByUserAndLessonAsync(string userId, int lessonId);
        Task<IEnumerable<LessonProgress>> GetByUserAsync(string userId);

    }
}
