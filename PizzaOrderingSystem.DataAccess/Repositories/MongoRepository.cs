
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using PizzaOrderSystem.DataAccess.Model.Contracts;
using PizzaOrderSystem.DataAccess.Model.Entities;
using System.Linq.Expressions;

namespace PizzaOrderSystem.DataAccess.Repositories;

public class MongoRepository<T> : IRepository<T> where T : IEntity
{
    private readonly IMongoCollection<T> _dbCollection;
    private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;
    private ILogger<MongoRepository<T>> _logger;

    public MongoRepository(IMongoDatabase database, string collectionName, ILogger<MongoRepository<T>> logger)
    {

        _dbCollection = database.GetCollection<T>(collectionName);
        _logger= logger;
    }


    public async Task<T> AddAsync(T entity)
    {
        await _dbCollection.InsertOneAsync(entity);
        return entity;
        
    }
    public async Task AddAllAsync(IEnumerable<T> entities)
    {
        await _dbCollection.InsertManyAsync(entities);

    }

    public async Task DeleteAsync(T entity)
    {
        FilterDefinition<T> filter = _filterBuilder.Eq(el => el.Id, entity.Id);
        await _dbCollection.DeleteOneAsync(filter);
    }


    public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
        FilterDefinition<T> filter = _filterBuilder.Where(expression);
        var entitys = _dbCollection.Find(filter).ToEnumerable();
        return Task.FromResult(entitys);

    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var entitys = await _dbCollection.Find(_filterBuilder.Empty).ToListAsync();
        return entitys;
    }

    public async Task<T> GetByAsync(Guid Id)
    {
        FilterDefinition<T> filter = _filterBuilder.Eq(entity => entity.Id, Id);
        var entity = await _dbCollection.Find(filter).FirstOrDefaultAsync();
        return entity;
    }


    public async Task UpdateAsync(T entity)
    {
        if (entity.Id != Guid.Empty)
        {
            await AddAsync(entity);
        }
        else
        {
            FilterDefinition<T> filter = _filterBuilder.Eq(el => el.Id, entity.Id);
            await _dbCollection.ReplaceOneAsync(filter, entity);
        }

    }

    

    

    
}
