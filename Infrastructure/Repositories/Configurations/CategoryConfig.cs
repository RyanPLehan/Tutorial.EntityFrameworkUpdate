using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutorial.EntityFrameworkUpdate.Domain.Models;

namespace Tutorial.EntityFrameworkUpdate.Infrastructure.Repositories.Configurations;

internal sealed class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasColumnName("CategoryID")
               .HasColumnType("INTEGER")
               .IsRequired(true)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
               .HasColumnName("Name")
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
    }
}
