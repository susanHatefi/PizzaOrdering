using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PizzaOrderSystem.DataAccess;
using PizzaOrderSystem.DataAccess.Model.Contracts;

namespace PizzaOrderingSystem.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private DB_Context _dbContext;
    private Dictionary<string, object> _repositories;
    private IServiceProvider _serviceProvider;
    private ILogger<UnitOfWork> _logger;
    public UnitOfWork(DB_Context dbContext, IServiceProvider serviceProvider,ILogger<UnitOfWork> logger)
    {
            _dbContext = dbContext;
        _serviceProvider= serviceProvider;
        _repositories = new Dictionary<string, object>();
        _logger =  logger;
    }

    public IRepository<T> Repository<T>() where T : class
    {
        var type= typeof(T).Name;
        if (!_repositories.ContainsKey(type))
        {
            var repository=_serviceProvider.GetRequiredService<IRepository<T>>();
            _repositories.Add(type, repository);
        }
        return (IRepository<T>) _repositories[type];

    }

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(SaveChangesAsync)}");
            throw new Exception("An error occured while saving changes");
        }
    
    }
    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
