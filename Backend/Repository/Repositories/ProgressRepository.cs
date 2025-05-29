using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypingWebApi.Data;
using TypingWebApi.Data.Context;

namespace Repository.Repositories
{
    public class ProgressRepository
        : Repository<LessonProgress>, IProgressRepository
    {
        public ProgressRepository(ApplicationContext ctx) : base(ctx) { }

        public async Task<LessonProgress> GetByUserAndLessonAsync(string userId, int lessonId) =>
            await Context.Set<LessonProgress>()
                .SingleOrDefaultAsync(x => x.UserId == userId && x.LessonId == lessonId);

        private ApplicationContext Context => (ApplicationContext)_context;
    }
}
