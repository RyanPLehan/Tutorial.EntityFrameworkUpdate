using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using Tutorial.EntityFrameworkUpdate.Infrastructure.Options;
using System.Runtime.InteropServices;
using System.Collections.Immutable;
using Microsoft.OpenApi.Extensions;

namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Registration;

public static class ServiceCollectionExtension
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.SectionName));

        // Caching
        services.AddMemoryCache();

        // These two connections are used only to keep the In-Memory databases alive throughout the demo.
        var dbOptions = configuration.GetSection(DatabaseOptions.SectionName).Get<DatabaseOptions>();
        services.AddKeyedSingleton<SqliteConnection>("InMemoryReadOnly", CreateInMemoryDatabases(dbOptions.MemoryRO));
        services.AddKeyedSingleton<SqliteConnection>("InMemoryReadWrite", CreateInMemoryDatabases(dbOptions.MemoryRW));


        /*
        // Database Context Factory and Repositories
        services.AddSingleton<IContextFactory<ManageContext>, ManageContextFactory>();

        // Manage
        // Non-Decorated Services
        //services.AddSingleton<IManage.ICategoryTypeRepository, CManage.CategoryTypeRepository>();
        //services.AddSingleton<IManage.ICypherTextRepository, CManage.CypherTextRepository>();
        //services.AddSingleton<IManage.IDecypherTextRepository, CManage.DecypherTextRepository>();
        services.AddSingleton<IManage.IWordRepository, CManage.WordRepository>();
        services.AddSingleton<IManage.IWordPartRepository, CManage.WordPartRepository>();
        services.AddSingleton<IManage.IWordPartReverseRepository, CManage.WordPartReverseRepository>();

        // Decoratored services (must use this pattern)
        // If decoratoring more than one, then try using Scrutor (https://github.com/khellang/Scrutor/tree/master)
        services.AddSingleton<CManage.CategoryTypeRepository>();        // Step 1 - Register the base implementation of Interface that will be decorated
        services.AddSingleton<IManage.ICategoryTypeRepository>(x =>     // Step 2 - Register the Decorator, using the required service from step 1 
            new DManage.CachedCategoryTypeRepository(x.GetRequiredService<IMemoryCache>(),
                                                     x.GetRequiredService<CManage.CategoryTypeRepository>())
        );

        services.AddSingleton<CManage.CypherTextRepository>();
        services.AddSingleton<IManage.ICypherTextRepository>(x =>
            new DManage.CachedCypherTextRepository(x.GetRequiredService<IMemoryCache>(),
                                                   x.GetRequiredService<CManage.CypherTextRepository>())
        );

        services.AddSingleton<CManage.DecypherTextRepository>();
        services.AddSingleton<IManage.IDecypherTextRepository>(x =>
            new DManage.CachedDecypherTextRepository(x.GetRequiredService<IMemoryCache>(),
                                                     x.GetRequiredService<CManage.DecypherTextRepository>())
            );



        // Search
        services.AddSingleton<ISearch.IWordRepository, CSearch.WordRepository>();
        services.AddSingleton<ISearch.IWordPartRepository, CSearch.WordPartRepository>();
        services.AddSingleton<ISearch.IWordPartReverseRepository, CSearch.WordPartReverseRepository>();
        services.AddSingleton<ISearch.IWordCategoryRepository, CSearch.WordCategoryRepository>();

        // Specialized Search Intersect Filter
        services.AddSingleton<CSearch.Filter.IWordRepository, CSearch.WordRepository>();
        services.AddSingleton<CSearch.Filter.IWordPartRepository, CSearch.WordPartRepository>();
        services.AddSingleton<CSearch.Filter.IWordPartReverseRepository, CSearch.WordPartReverseRepository>();
        services.AddSingleton<CSearch.Filter.IWordCategoryRepository, CSearch.WordCategoryRepository>();



        // Aggregate
        // Non Decoratored services
        services.AddSingleton<IAggregate.IWordCategoryRepository, CAggregate.WordCategoryRepository>();
        //services.AddSingleton<IAggregate.IWordPartRepository, CAggregate.WordPartRepository>();
        //services.AddSingleton<IAggregate.IWordPartReverseRepository, CAggregate.WordPartReverseRepository>();
        //services.AddSingleton<IAggregate.IWordRepository, CAggregate.WordRepository>();

        // Decoratored services (must use this pattern)
        // If decoratoring more than one, then try using Scrutor (https://github.com/khellang/Scrutor/tree/master)
        services.AddSingleton<CAggregate.WordPartRepository>();         // Step 1 - Register the base implementation of Interface that will be decorated
        services.AddSingleton<IAggregate.IWordPartRepository>(x =>      // Step 2 - Register the Decorator, using the required service from step 1 
            new DAggregate.CachedWordPartRepository(x.GetRequiredService<IMemoryCache>(),
                                                    x.GetRequiredService<CAggregate.WordPartRepository>())
            );

        services.AddSingleton<CAggregate.WordPartReverseRepository>();
        services.AddSingleton<IAggregate.IWordPartReverseRepository>(x =>
            new DAggregate.CachedWordPartReverseRepository(x.GetRequiredService<IMemoryCache>(),
                                                           x.GetRequiredService<CAggregate.WordPartReverseRepository>())
            );

        services.AddSingleton<CAggregate.WordRepository>();
        services.AddSingleton<IAggregate.IWordRepository>(x =>
            new DAggregate.CachedWordRepository(x.GetRequiredService<IMemoryCache>(),
                                                x.GetRequiredService<CAggregate.WordRepository>())
            );

        */
        return services;
    }

    private static SqliteConnection CreateInMemoryDatabases(string inMemoryConnStr)
    {
        const string RESOURCE_NAME = "Inventory.sql";
        string resourceFQN = GetResourceFQN(RESOURCE_NAME);

        SqliteConnection connection = new SqliteConnection(inMemoryConnStr);
        connection.Open();

        SqliteCommand command = new SqliteCommand();

        command.CommandType = System.Data.CommandType.Text;
        command.CommandText = LoadFromResource(resourceFQN);
        command.Connection = connection;
        command.ExecuteNonQuery();

        return connection;
    }

    private static string GetResourceFQN(string resourceName)
    {
        var assembly = typeof(ServiceCollectionExtension).Assembly;
        var resourceNames = assembly.GetManifestResourceNames();

        return resourceNames.Where(x => x.EndsWith(resourceName, StringComparison.OrdinalIgnoreCase))
                            .First();
    }

    private static string LoadFromResource(string resourceFQN)
    {
        string resourceData;
        var assembly = typeof(ServiceCollectionExtension).Assembly;

        using (Stream stream = assembly.GetManifestResourceStream(resourceFQN))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                resourceData = reader.ReadToEnd();
            }
        }

        return resourceData;
    }
}

