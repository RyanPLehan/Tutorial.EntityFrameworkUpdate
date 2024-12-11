using Microsoft.EntityFrameworkCore;
using System.Data;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories.Configurations;

namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories;

internal sealed class ReadOnlyContext : DbContext
{
    public ReadOnlyContext(DbContextOptions<ReadOnlyContext> options)
        : base(options)
    { }


    internal DbSet<Category> Categories { get; set; }


    public override int SaveChanges() => throw new ReadOnlyException();

    public override int SaveChanges(bool acceptAllChangesOnSuccess) => throw new ReadOnlyException();

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default) => throw new ReadOnlyException();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => throw new ReadOnlyException();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfig());
    }
}
