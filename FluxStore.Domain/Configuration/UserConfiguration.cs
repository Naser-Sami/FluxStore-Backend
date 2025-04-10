using FluxStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluxStore.Domain.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .HasColumnType("VARCHAR(36)")
                   .IsRequired();

            builder.Property(u => u.UserName)
                   .HasColumnType("VARCHAR")
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.Email)
                   .HasColumnType("VARCHAR")
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.PasswordHash)
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(64) // FIXED: Ensuring enough space for hash
                   .IsRequired();

            builder.Property(u => u.Role)
                   .HasColumnType("INTEGER")
                   .IsRequired()
                   .HasDefaultValue(UserRole.User);

            builder.Property(u => u.RefreshToken)
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(128) // Just in case
                   .IsRequired(false);

            builder.Property(u => u.RefreshTokenExpiryTime)
                   .HasColumnType("DATETIME")
                   .IsRequired(false);

            builder.Property(u => u.ResetPasswordToken)
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(128) // Just in case
                   .IsRequired(false);

            builder.Property(u => u.ResetPasswordExpiry)
                   .HasColumnType("DATETIME")
                   .IsRequired(false);
        }
    }
}

