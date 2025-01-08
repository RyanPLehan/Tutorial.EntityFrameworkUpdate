using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutorial.EntityFrameworkUpdate.Application.Models;

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
               .HasColumnType("INT")
               .IsRequired(true)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
               .HasColumnName("Name")
               .HasColumnType("varchar")
               .HasMaxLength(75)
               .IsRequired(true)
               .IsUnicode(false);

        builder.Property(x => x.Description)
               .HasColumnName("Description")
               .HasColumnType("varchar")
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
               .HasColumnType("INT")
               .IsRequired(true)
               .ValueGeneratedNever();

        builder.Property(x => x.CategoryId)
               .HasColumnName("CategoryID")
               .HasColumnType("INT")
               .IsRequired(true)
               .ValueGeneratedNever();
    }
}
