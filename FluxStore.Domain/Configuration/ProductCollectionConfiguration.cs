using System.Reflection.Emit;
using FluxStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxStore.Domain.Configuration
{
    public class ProductCollectionConfiguration : IEntityTypeConfiguration<ProductCollection>
    {
        public void Configure(EntityTypeBuilder<ProductCollection> builder)
        {
            builder.HasKey(
                pc => new { pc.ProductId, pc.CollectionId });

            builder
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCollections)
                .HasForeignKey(pc => pc.ProductId);

            builder
                .HasOne(pc => pc.Collection)
                .WithMany(c => c.ProductCollections)
                .HasForeignKey(pc => pc.CollectionId);
        }
    }
}