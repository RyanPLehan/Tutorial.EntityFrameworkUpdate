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
    internal DbSet<Product> Products { get; set; }
    internal DbSet<ProductTag> ProductTags { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfig());
        modelBuilder.ApplyConfiguration(new ProductConfig());
        modelBuilder.ApplyConfiguration(new ProductTagConfig());

        /*
        modelBuilder.Entity<Category>().HasNoKey();
        modelBuilder.Entity<Product>().Ignore(e => e.Tags);
        modelBuilder.Entity<Product>().HasNoKey();
        modelBuilder.Entity<Tag>().HasNoKey();
        */
    }
}
