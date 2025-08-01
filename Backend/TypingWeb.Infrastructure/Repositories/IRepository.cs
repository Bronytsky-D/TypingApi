using System.Linq.Expressions;
using TypingWeb.Common;

namespace TypingWeb.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        //Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IExecutionResponse> AddAsync(T entity);
        Task<IExecutionResponse> Remove(T entity);
        Task<IExecutionResponse> Update(T entity);
    }
}
