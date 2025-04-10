using FluxStore.Domain.Configuration;
using FluxStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FluxStore.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<ProductCollection> ProductCollections { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CollectionConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCollectionConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}