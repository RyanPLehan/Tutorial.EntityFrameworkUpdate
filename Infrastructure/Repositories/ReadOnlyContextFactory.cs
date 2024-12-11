using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Runtime;
using Tutorial.EntityFrameworkUpdate.Infrastructure.Options;

namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories;

internal class ReadOnlyContextFactory : IContextFactory<ReadOnlyContext>
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly DatabaseOptions _options;


    public ReadOnlyContextFactory(ILoggerFactory loggerFactory,
                                  IOptions<DatabaseOptions> options)
        : this(loggerFactory, options?.Value)
    { }


    public ReadOnlyContextFactory(ILoggerFactory loggerFactory,
                                  DatabaseOptions? options)
    {
        ArgumentNullException.ThrowIfNull(loggerFactory, nameof(loggerFactory));
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        _loggerFactory = loggerFactory;
        _options = options;
    }


    ReadOnlyContext IContextFactory<ReadOnlyContext>.CreateCommandContext()
    {
        throw new NotImplementedException();
    }

    ReadOnlyContext IContextFactory<ReadOnlyContext>.CreateQueyContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ReadOnlyContext>()
                                        .UseLoggerFactory(_loggerFactory)
                                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                                        .UseSqlite(BuildConnectionString(_options.ReadOnly), AddDatabaseOptions)
                                        .EnableSensitiveDataLogging();

        return new ReadOnlyContext(optionsBuilder.Options);
    }

    /// <summary>
    /// Build Sqlite Connection string
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// See: https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/connection-strings
    /// See: https://www.sqlite.org/wal.html
    /// </remarks>
    private string BuildConnectionString(string dataSource)
    {
        return new SqliteConnectionStringBuilder()
        {
            Mode = SqliteOpenMode.Memory,
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
