using Microsoft.EntityFrameworkCore;
using System.Data;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;
using Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories.Configurations;

namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories;

internal sealed class InventoryContext : DbContext
{
    public InventoryContext(DbContextOptions<InventoryContext> options)
        : base(options)
    { }


    internal DbSet<Category> Categories { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfig());
    }
}
