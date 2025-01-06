using Microsoft.EntityFrameworkCore;
using System.Data;
using Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories.Models;
using Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories.Configurations;

namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories;

internal sealed class InventoryUpdateContext : DbContext
{
    public InventoryUpdateContext(DbContextOptions<InventoryUpdateContext> options)
        : base(options)
    { }


    internal DbSet<PerformantProductUpdate> Products { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PerformantProductConfig());
    }
}
