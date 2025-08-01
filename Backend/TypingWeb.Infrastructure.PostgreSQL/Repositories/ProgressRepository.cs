using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TypingWeb.Common;
using TypingWeb.Domain;
using TypingWeb.Domain.Models.Entities;
using TypingWeb.Infrastructure.PostgreSQL.Models;
using TypingWeb.Infrastructure.Repositories;

namespace TypingWeb.Infrastructure.PostgreSQL.Repositories
{
    public class ProgressRepository(ApplicationContext ctx, IMapper autoMapper) : IProgressRepository
    {
        private readonly ApplicationContext _context = ctx;
        private readonly IMapper _mapper = autoMapper;

        public async Task<IExecutionResponse> AddAsync(LessonProgressEntity model)
        {
            var entity = _mapper.Map<LessonProgress>(model);
            await _context.Set<LessonProgress>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return ExecutionResponse.Successful(entity.Id);
        }

        public async Task<IEnumerable<LessonProgressEntity>> GetAllAsync()
        {
            var entites = await _context.Set<LessonProgress>().ToListAsync();

            return _mapper.Map<IEnumerable<LessonProgressEntity>>(entites);
        }


        public async Task<LessonProgressEntity> GetByUserAndLessonAsync(string userId, int lessonId)
        {
            var entity = await _context.Set<LessonProgress>()
                .SingleOrDefaultAsync(x => x.UserId == userId && x.LessonId == lessonId);

            return _mapper.Map<LessonProgressEntity>(entity);
        }

        public async Task<IEnumerable<LessonProgressEntity>> GetByUserAsync(string userId)
        {
            var entites = await _context.Set<LessonProgress>()
                            .Where(x => x.UserId == userId)
                            .ToListAsync();

            return _mapper.Map<IEnumerable<LessonProgressEntity>>(entites);
        }

        public async Task<IExecutionResponse> Remove(LessonProgressEntity model)
        {
            var entity = _mapper.Map<LessonProgress>(model);
            _context.Set<LessonProgress>().Remove(entity);
            await _context.SaveChangesAsync();

            return ExecutionResponse.Successful(entity.Id);
        }

        public async Task<IExecutionResponse> Update(LessonProgressEntity model)
        {
            var entity = _mapper.Map<LessonProgress>(model);
            _context.Set<LessonProgress>().Update(entity);
            await _context.SaveChangesAsync();

            return ExecutionResponse.Successful(entity.Id);
        }
    }
}
