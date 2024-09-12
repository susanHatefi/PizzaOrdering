using System.Linq.Expressions;

namespace PizzaOrderSystem.DataAccess.Model.Contracts;

public interface IRepository<T>
{
    public Task<T> AddAsync(T entity);
    Task AddAllAsync(IEnumerable<T> entities);
    public Task UpdateAsync(T entity);
    public Task DeleteAsync(T entity);
    public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<T> GetByAsync(Guid Id);

}
