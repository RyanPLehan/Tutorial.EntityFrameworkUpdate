using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories.Models;

namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories.Configurations;

internal sealed class PerformantProductConfig : IEntityTypeConfiguration<PerformantProductUpdate>
{
    public void Configure(EntityTypeBuilder<PerformantProductUpdate> builder)
    {
        builder.ToTable("Product");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasColumnName("ProductID")
               .HasColumnType("INTEGER")
               .IsRequired(true)
               .ValueGeneratedOnAdd();

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
    }
}
