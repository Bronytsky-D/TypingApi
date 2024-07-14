using System.Linq.Expressions;

namespace TypingWebApi.Data
{
    public interface IRepository<T>
    {
       // IQueryable<T> FindAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
    }
}
