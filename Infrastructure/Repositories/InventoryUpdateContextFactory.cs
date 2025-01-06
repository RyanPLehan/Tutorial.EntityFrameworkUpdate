using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Runtime;
using Tutorial.EntityFrameworkUpdate.Infrastructure.Options;

namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories;

internal class InventoryUpdateContextFactory : IContextFactory<InventoryUpdateContext>
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly DatabaseOptions _options;


    public InventoryUpdateContextFactory(ILoggerFactory loggerFactory,
                                  IOptions<DatabaseOptions> options)
        : this(loggerFactory, options?.Value)
    { }


    public InventoryUpdateContextFactory(ILoggerFactory loggerFactory,
                                  DatabaseOptions? options)
    {
        ArgumentNullException.ThrowIfNull(loggerFactory, nameof(loggerFactory));
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _loggerFactory = loggerFactory;
        _options = options;
    }


    InventoryUpdateContext IContextFactory<InventoryUpdateContext>.CreateCommandContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<InventoryUpdateContext>()
                                        .UseLoggerFactory(_loggerFactory)
                                        .UseSqlite(BuildConnectionString(_options.Inventory, SqliteOpenMode.Memory), AddDatabaseOptions)      // In Memory
                                        //.UseSqlite(BuildConnectionString(BuildDataSource(_options.InventoryRO), SqliteOpenMode.ReadOnly), AddDatabaseOptions)   // On Disk
                                        .EnableSensitiveDataLogging();

        return new InventoryUpdateContext(optionsBuilder.Options);
    }

    InventoryUpdateContext IContextFactory<InventoryUpdateContext>.CreateQueyContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<InventoryUpdateContext>()
                                        .UseLoggerFactory(_loggerFactory)
                                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                                        .UseSqlite(BuildConnectionString(_options.Inventory, SqliteOpenMode.Memory), AddDatabaseOptions)      // In Memory
                                                                                                                                              //.UseSqlite(BuildConnectionString(BuildDataSource(_options.InventoryRO), SqliteOpenMode.ReadOnly), AddDatabaseOptions)   // On Disk
                                        .EnableSensitiveDataLogging();

        return new InventoryUpdateContext(optionsBuilder.Options);
    }


    private static string BuildDataSource(string databaseName)
    {
        var dbFileName = String.Format("{0}.db3", databaseName);
        return Path.Combine(Path.GetTempPath(), dbFileName);
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

    private void AddDatabaseOptions(SqliteDbContextOptionsBuilder builder)
    {
        builder.CommandTimeout(60)
               .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
    }

}
