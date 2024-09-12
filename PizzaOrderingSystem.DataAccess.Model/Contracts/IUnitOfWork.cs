namespace PizzaOrderSystem.DataAccess.Model.Contracts;

public interface IUnitOfWork : IDisposable 
{
    public IRepository<T> Repository<T>() where T : class;
    public Task<int> SaveChangesAsync();
}
