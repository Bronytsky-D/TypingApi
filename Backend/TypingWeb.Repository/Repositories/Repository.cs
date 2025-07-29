using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Repository.Context;

namespace Repository.Repositories
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
        public async Task AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
