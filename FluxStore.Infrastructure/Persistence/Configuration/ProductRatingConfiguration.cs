using FluxStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxStore.Infrastructure.Persistence.Configuration
{
    public class ProductRatingConfiguration : IEntityTypeConfiguration<ProductRating>
    {
        public void Configure(EntityTypeBuilder<ProductRating> builder)
        {
            builder.ToTable("ProductRatings");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Rating)
                .IsRequired()
                .HasDefaultValue(1)
                .HasComment("Rating value from 1 to 5");

            builder.HasOne(r => r.Product)
                .WithMany(p => p.Ratings)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

