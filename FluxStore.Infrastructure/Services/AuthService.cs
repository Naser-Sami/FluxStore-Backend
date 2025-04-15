using System.Security.Cryptography;
using FluxStore.Application.Common.Interfaces;
using FluxStore.Application.DTOs.Auth;
using FluxStore.Application.Interfaces;
using FluxStore.Domain.Entities;
using FluxStore.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace FlxStore.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher<UserEntity> _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthService(AppDbContext context,
        ITokenService tokenService)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<UserEntity>();
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        if (_context.Users.Any(u => u.Email == request.Email))
            throw new Exception("Email already in use");

        if (request.Password != request.ConfirmPassword)
            throw new Exception("Passwords do not match");

        var user = new UserEntity
        {
            Username = request.Username,
            Email = request.Email,
            FirstName = "",
            LastName = "",
            PasswordHash = "",
            Gender = "",
            PhoneNumber = "",
            Address = "",
            ImageUrl = "",
            Role = "Customer",
            RefreshToken = "",
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
    };

        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
        user.RefreshToken = GenerateRefreshToken();

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return await Response(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);

        if (user == null)
            throw new Exception("Invalid credentials");

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result == PasswordVerificationResult.Failed)
            throw new Exception("Invalid credentials");

        return await Response(user);
    }

    private Task<AuthResponse> Response(UserEntity user)
    {
        return Task.FromResult(new AuthResponse
        {
            Token = _tokenService.CreateToken(user),
            RefreshToken = user.RefreshToken,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role
        });
    }

    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}