using FluxStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxStore.Domain.Configuration
{
	public class CollectionConfiguration : IEntityTypeConfiguration<Collection>
    {
		public void Configure(EntityTypeBuilder<Collection> builder)
		{
		}
	}
}

