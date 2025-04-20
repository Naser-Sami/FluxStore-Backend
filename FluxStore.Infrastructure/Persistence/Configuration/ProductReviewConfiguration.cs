using FluxStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxStore.Infrastructure.Persistence
{
	public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> builder)
        {
            builder.ToTable("ProductReviews");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.ReviewerName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(r => r.Description)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(r => r.Date)
                .IsRequired();

            builder.Property(r => r.Rating)
                .IsRequired();

            builder.Property(r => r.ReviewerImage)
                .HasMaxLength(1000);

            builder.Property(r => r.Images)
                .HasConversion(
                    v => v != null ? string.Join(';', v) : null,
                    v => string.IsNullOrEmpty(v) ? new List<string>() : v.Split(';',
                        StringSplitOptions.RemoveEmptyEntries).ToList()
                )
                .HasColumnName("Images");

            builder.HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

