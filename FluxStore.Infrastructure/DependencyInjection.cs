using FluxStore.Application.Common.Interfaces;
using FluxStore.Application.Interfaces;
using FluxStore.Domain.Interfaces;
using FluxStore.Domain.Repositories;
using FluxStore.Infrastructure.Persistence;
using FluxStore.Infrastructure.Persistence.Repositories;
using FluxStore.Infrastructure.Repositories;
using FluxStore.Infrastructure.Services;
using FlxStore.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlxStore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddHttpContextAccessor();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<AppDbContext>()!);

        return services;
    }
}