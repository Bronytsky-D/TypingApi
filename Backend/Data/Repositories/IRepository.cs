using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}
