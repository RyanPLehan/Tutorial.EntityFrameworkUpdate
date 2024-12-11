using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Categories;
using Tutorial.EntityFrameworkUpdate.Infrastructure.Options;
using Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories;
using Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories.Decorators;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http.HttpResults;

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
        //CreateOnDiskDatabase(dbOptions.ReadOnly);
        //CreateOnDiskDatabase(dbOptions.ReadWrite);
        services.AddKeyedSingleton<SqliteConnection>("InMemoryReadOnly", CreateInMemoryDatabase(dbOptions.InventoryRO));
        services.AddKeyedSingleton<SqliteConnection>("InMemoryReadWrite", CreateInMemoryDatabase(dbOptions.InventoryRW));



        // Database Context Factories
        services.AddSingleton<IContextFactory<ReadOnlyContext>, ReadOnlyContextFactory>();

        // Non-Decorated
        //services.AddSingleton<ICategoryRepository, CategoryRepository>();


        // Decorated (must use this pattern)
        // Step 1 - Register the base implementation of Interface that will be decorated
        // Step 2 - Register the Decorator, using the required service from step 1 
        // If decoratoring more than one, then try using Scrutor (https://github.com/khellang/Scrutor/tree/master)
        services.AddSingleton<CategoryRepository>();
        services.AddSingleton<ICategoryRepository>(x =>
            new CachedCategoryRepository(x.GetRequiredService<IMemoryCache>(),
                                         x.GetRequiredService<CategoryRepository>())
        );


        return services;
    }

    private static SqliteConnection CreateInMemoryDatabase(string databaseName)
    {
        const string RESOURCE_NAME = "Inventory.sql";
        string resourceFQN = GetResourceFQN(RESOURCE_NAME);

        SqliteConnection connection = new SqliteConnection(BuildConnectionString(databaseName, SqliteOpenMode.Memory));
        connection.Open();

        SqliteCommand command = new SqliteCommand();

        command.CommandType = System.Data.CommandType.Text;
        command.CommandText = LoadFromResource(resourceFQN);
        command.Connection = connection;
        command.ExecuteNonQuery();

        return connection;
    }

    private static void CreateOnDiskDatabase(string databaseName)
    {
        const string RESOURCE_NAME = "Inventory.sql";
        string resourceFQN = GetResourceFQN(RESOURCE_NAME);

        var dataSource = BuildDataSource(databaseName);
        using (SqliteConnection connection = new SqliteConnection(BuildConnectionString(dataSource, SqliteOpenMode.ReadWriteCreate)))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();

            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = LoadFromResource(resourceFQN);
            command.Connection = connection;
            command.ExecuteNonQuery();
        }
    }

    private static string BuildDataSource(string databaseName)
    {
        var dbFileName = String.Format("{0}.db3", databaseName);
        var pathName = Path.Combine(Path.GetTempPath(), dbFileName);

        if (File.Exists(pathName))
            File.Delete(pathName);

        return pathName;
    }

    /// <summary>
    /// Build Sqlite Connection string
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// See: https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/connection-strings
    /// See: https://www.sqlite.org/wal.html
    /// </remarks>
    private static string BuildConnectionString(string dataSource, SqliteOpenMode mode = SqliteOpenMode.Memory)
    {
        return new SqliteConnectionStringBuilder()
        {
            Mode = mode,
            DataSource = dataSource,
            Pooling = true,
            DefaultTimeout = 30,
            Cache = SqliteCacheMode.Shared,         // Do NOT use with Write-Ahead Logging
        }.ToString();
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

