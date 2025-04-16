using FluxStore.Application.Interfaces;
using FluxStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FluxStore.Infrastructure.Persistence
{
	public class AppDbContext : DbContext, IApplicationDbContext
    {
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<Role> Roles => Set<Role>();

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductRating> Ratings { get; set; }
        public DbSet<ProductReview> Reviews { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

