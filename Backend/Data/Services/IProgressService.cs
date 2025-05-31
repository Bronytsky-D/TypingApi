using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypingWebApi.Domains.Models.Types;

namespace Domain.Services
{
    public interface IProgressService
    {
        Task<IExecutionResponse> GetByUserAndLessonIdAsync(string userId, int lessonId);
        Task<IExecutionResponse> GetByUserIdAsync(string userId);
        Task<IExecutionResponse> UpsertAsync(LessonProgress entity);
    }
}
