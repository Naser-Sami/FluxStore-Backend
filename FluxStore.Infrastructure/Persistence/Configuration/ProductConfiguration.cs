using FluxStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxStore.Infrastructure.Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Description).HasMaxLength(2000);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.ImageUrl).HasMaxLength(1000);
            builder.Property(p => p.Stock).IsRequired();

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(p => p.Ratings)
                .WithOne(r => r.Product)
                .HasForeignKey(r => r.ProductId);

            builder
                .HasMany(p => p.Reviews)
                .WithOne(r => r.Product)
                .HasForeignKey(r => r.ProductId);

            // Value conversion for lists
            builder
                .Property(p => p.AdditionalImages)
                .HasConversion(
                    v => string.Join(";", v),
                    v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                );

            builder
                .Property(p => p.AvailableColors)
                .HasConversion(
                    v => string.Join(";", v),
                    v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                );

            builder
                .Property(p => p.AvailableSizes)
                .HasConversion(
                    v => string.Join(";", v),
                    v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
        }
    }
}

