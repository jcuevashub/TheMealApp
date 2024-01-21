using System.Linq.Expressions;

namespace Auth.Application.Interfaces.Repositories;

public interface IGenericRepositoryAsync<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task DeleteAsync(T entity);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<IReadOnlyList<T>> FindByCondition(Expression<Func <T, bool>> expression);
}
