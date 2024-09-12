using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PizzaOrderingSystem.DataAccess;
using PizzaOrderSystem.DataAccess.Model.Contracts;
using PizzaOrderSystem.DataAccess.Model.Entities;
using PizzaOrderSystem.DataAccess.Model.Settings;
using PizzaOrderSystem.DataAccess.Repositories;

namespace PizzaOrderSystem.DataAccess;

public static class DataAccessDI
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddOptions<SqlSetting>().Configure<IConfiguration>((options, config) =>
        {
            config.GetSection(nameof(SqlSetting)).Bind(options);
        });

        services.AddOptions<MongoDBSetting>().Configure<IConfiguration>((options, config) =>
        {
            config.GetSection(nameof(MongoDBSetting)).Bind(options);
        });

        services.AddDbContextPool<DB_Context>((provider, options) =>
        {
            if (!options.IsConfigured)
            {
                options.UseLazyLoadingProxies();
                var sqlSetting=provider.GetService<IOptions<SqlSetting>>()?.Value;
                    options.UseSqlServer(sqlSetting?.ConnectionString).EnableSensitiveDataLogging();
            }
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static IServiceCollection AddRepository<T>(this IServiceCollection services) where T: Entity, new(){
        services.AddTransient<IRepository<T>>((provider) =>
        {
            var sqlSetting = provider.GetService<IOptions<SqlSetting>>()?.Value;
            var logger=provider.GetRequiredService<ILogger<SqlRepository<T>>>();
            if (sqlSetting != null && sqlSetting.IsEnable) {
                var dbContext=provider.GetRequiredService<DB_Context>();
                return new SqlRepository<T>(dbContext,logger);
            }
            var logger2 = provider.GetRequiredService<ILogger<InMemoryRepository<T>>>();

            return new InMemoryRepository<T>(logger2);

        });
        return services;
    }

    public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName) where T : IEntity
    {

        services.AddSingleton(provider =>
        {
            var mongoSetting = provider.GetService<IOptions<MongoDBSetting>>()?.Value;
            var mongoClient = new MongoClient(mongoSetting?.ConnectionString);
            var dataBase = mongoClient.GetDatabase(mongoSetting?.DataBase);
            return dataBase;

        });
        services.AddSingleton(provider =>
        {
            var logger = provider.GetRequiredService<ILogger<MongoRepository<T>>>();
            var dataBase = provider.GetRequiredService<IMongoDatabase>();
            return new MongoRepository<T>(dataBase, collectionName,logger);
        });

        return services;
    }
}
