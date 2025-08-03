using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TypingWeb.Common;
using TypingWeb.Domain;
using TypingWeb.Infrastructure.Repositories;

namespace TypingWeb.Infrastructure.PostgreSQL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;
        public Repository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var tmp = await _context.Set<T>().Where(predicate).ToListAsync();
            return tmp;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var tmp = await _context.Set<T>().ToListAsync();
            return tmp;
        }
        public async Task<IExecutionResponse> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            return ExecutionResponse.Successful(entity);
        }
        public async Task<IExecutionResponse> RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return ExecutionResponse.Successful(entity);
        }
        public async Task<IExecutionResponse> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

            return ExecutionResponse.Successful(entity);
        }
    }
}
