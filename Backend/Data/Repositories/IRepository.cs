using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        // IQueryable<T> FindAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        public Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        public void Remove(T entity);

    }
}
