using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutorial.EntityFrameworkUpdate.Application.Models;

namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories.Configurations;

internal sealed class ProductTagConfig : IEntityTypeConfiguration<ProductTag>
{
    public void Configure(EntityTypeBuilder<ProductTag> builder)
    {
        builder.ToTable("ProductTag");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasColumnName("TagID")
               .HasColumnType("INT")
               .IsRequired(true)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
               .HasColumnName("Name")
               .HasColumnType("varchar")
               .HasMaxLength(25)
               .IsRequired(true)
               .IsUnicode(false);

        builder.Property(x => x.Value)
               .HasColumnName("Value")
               .HasColumnType("varchar")
               .HasMaxLength(50)
               .IsRequired(false)
               .IsUnicode(false);

        builder.Property(x => x.ProductId)
               .HasColumnName("ProductID")
               .HasColumnType("INT")
               .IsRequired(true)
               .ValueGeneratedNever();
    }
}
