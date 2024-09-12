using Microsoft.Extensions.Logging;
using PizzaOrderSystem.DataAccess.Model.Contracts;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace PizzaOrderSystem.DataAccess.Repositories;

public class InMemoryRepository<T> : IRepository<T> where T : IEntity
{
    public static ConcurrentDictionary<Guid, T> CollectionOfData { get; } = new();
    private ILogger<InMemoryRepository<T>> _logger;

    public InMemoryRepository(ILogger<InMemoryRepository<T>> logger)
    {
        _logger= logger;
    }

    public Task<T> AddAsync(T entity)
    {
        CollectionOfData.TryAdd(entity.Id, entity);
        return Task.FromResult(entity);
    }
    public async Task AddAllAsync(IEnumerable<T> entities)
    {
        var insertedEntity = new List<T>();
        try
        {
            foreach(var entity in entities)
            {
                await AddAsync(entity);
                insertedEntity.Add(entity);
            }

        }
        catch (Exception ex)
        {
            foreach (var entity in insertedEntity)
            {
                await DeleteAsync(entity);
            }
        }
        finally
        {
            insertedEntity.Clear();
        }

    }
    public Task DeleteAsync(T entity)
    {
        var item = CollectionOfData.First(item => item.Key == entity.Id);
        CollectionOfData.TryRemove(item);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
        var items = CollectionOfData.Values.AsQueryable().Select(item => item).AsEnumerable();
        return Task.FromResult(items);
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        var items = CollectionOfData.Values.Select(item => item).ToArray();
        return Task.FromResult<IEnumerable<T>>(items);
    }

    public Task<T> GetByAsync(Guid Id)
    {
        var item = CollectionOfData[Id];
        return Task.FromResult(item);
    }


    public Task UpdateAsync(T entity)
    {
        var item = CollectionOfData[entity.Id];
        if (item != null)
        {
            DeleteAsync(item);
        }
        AddAsync(entity);
        return Task.CompletedTask;

    }

   
}
