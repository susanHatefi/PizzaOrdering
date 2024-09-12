using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaOrderSystem.DataAccess.Model.Contracts;
using PizzaOrderSystem.DataAccess.Model.Entities;
using System.Linq.Expressions;

namespace PizzaOrderSystem.DataAccess.Repositories;

public class SqlRepository<T> : IRepository<T> where T : Entity,new()
{
    private DB_Context _dbContext;
    private ILogger<SqlRepository<T>> _logger;
    private DbSet<T> _dbSet;
    public SqlRepository(DB_Context dbCotext, ILogger<SqlRepository<T>> logger) 
    {
        _dbContext = dbCotext;
        _logger= logger;
        _dbSet = _dbContext.Set<T>();
    }
    public async Task<T> AddAsync(T entity)
    {
        try
        {
           await _dbContext.AddAsync(entity);
            return entity;
        }
        catch (Exception ex) {
            _logger.LogError(ex,$"SQL Insert Error");
            throw  new Exception("An error occured while creating a new entity");
        }
    }

    public async Task AddAllAsync(IEnumerable<T> entities)
    {
        try
        {
            foreach (var entity in entities) {
               var entityState= _dbContext.Entry<T>(entity).IsKeySet ? EntityState.Unchanged : EntityState.Added;
                _dbContext.Entry<T>(entity).State = entityState;
            }
            await _dbContext.AddRangeAsync(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"SQL Insert All Error");
            throw new Exception("An error occured while creating a range of entities");
        }
    }

    public async Task DeleteAsync(T entity)
    {
        try
        {
            await AttachEntity(entity);
            _dbSet.Remove(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,$"SQL Delete Error {entity.Id}");
            throw new Exception("An error occured while deleting the entity");

        }
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
        try
        {
            var items = await _dbSet.Where(expression).Select(item => item).ToListAsync<T>();
            return items;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SQL Find Error");
            throw new Exception("An error occured while finding data");

        }
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        try
        {
                var items = await _dbSet.Select(item => item).ToListAsync();
                return items;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SQL Fetch All Data Error");
            throw new Exception("An error occured while fetching all Data ");

        }
    }

    public  Task<T> GetByAsync(Guid Id)
    {
        try
        {
            var item = _dbSet.AsNoTracking().First(item => item.Id == Id);
            return Task.FromResult(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"SQL Fetch Error Entity {Id}");
            throw new Exception("An error occured while fetching the entity");

        }
    }



    public async  Task UpdateAsync(T entity)
    {
        try
        {
            await AttachEntity(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"SQL Update Error {entity.Id}");
            throw new Exception("An error occured while updating the entity");

        }
    }

    public  Task AttachEntity(T entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        return Task.CompletedTask;
    }


}
