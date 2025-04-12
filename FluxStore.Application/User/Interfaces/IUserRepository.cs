using FluxStore.Domain.Entities;

namespace FluxStore.Domain.Repositories;

public interface IUserRepository
{
    Task<UserEntity?> GetByIdAsync(Guid id);
    Task<UserEntity?> GetByEmailAsync(string email);
    Task AddAsync(UserEntity user);
    Task UpdateAsync(UserEntity user);
    Task<bool> ExistsByEmailAsync(string email);
    Task<UserEntity?> GetCurrentUserAsync();
}