using System.Linq.Expressions;
namespace ToDoListApp.DAL.Repositories.Interfaces;

public interface IRepository<T>
    where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<T?> GetSingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}