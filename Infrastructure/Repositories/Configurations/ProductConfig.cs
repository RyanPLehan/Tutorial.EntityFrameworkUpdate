using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutorial.EntityFrameworkUpdate.Domain.Inventory.Models;

namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories.Configurations;

internal sealed class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");
        builder.HasKey(x => x.Id);
        builder.Ignore(x => x.Tags);

        builder.Property(x => x.Id)
               .HasColumnName("ProductID")
               .HasColumnType("INTEGER")
               .IsRequired(true)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
               .HasColumnName("ProductName")
               .HasColumnType("TEXT")
               .HasMaxLength(75)
               .IsRequired(true)
               .IsUnicode(false);

        builder.Property(x => x.Description)
               .HasColumnName("Description")
               .HasColumnType("TEXT")
               .HasMaxLength(255)
               .IsRequired(true)
               .IsUnicode(false);

        builder.Property(x => x.Price)
               .HasColumnName("Price")
               .HasColumnType("NUMERIC")
               .HasPrecision(7, 2)
               .IsRequired(true);

        builder.Property(x => x.Quantity)
               .HasColumnName("Quantity")
               .HasColumnType("INTEGER")
               .IsRequired(true)
               .ValueGeneratedNever();

        builder.Property(x => x.CategoryId)
               .HasColumnName("CategoryID")
               .HasColumnType("INTEGER")
               .IsRequired(true)
               .ValueGeneratedNever();
    }
}
