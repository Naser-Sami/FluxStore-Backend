using FluxStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FluxStore.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<UserEntity> Users { get; }
        public DbSet<Role> Roles { get; }
        public DbSet<Product> Products { get; }
        public DbSet<Category> Categories { get; }
        public DbSet<ProductRating> Ratings { get; }
        public DbSet<ProductReview> Reviews { get; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

