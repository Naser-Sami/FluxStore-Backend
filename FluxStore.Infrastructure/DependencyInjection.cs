﻿using FluxStore.Application.Common.Interfaces;
using FluxStore.Application.Interfaces;
using FluxStore.Domain.Repositories;
using FluxStore.Infrastructure.Persistence;
using FluxStore.Infrastructure.Persistence.Repositories;
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
        services.AddSingleton<ITokenService, TokenService>();
        services.AddHttpContextAccessor();

        return services;
    }
}