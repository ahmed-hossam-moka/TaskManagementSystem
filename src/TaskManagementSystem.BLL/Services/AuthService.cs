using Microsoft.AspNetCore.Identity;
using TaskManagementSystem.DAL.Common;
using TaskManagementSystem.BLL.DTOs;
using TaskManagementSystem.BLL.Interfaces;
using TaskManagementSystem.DAL.Entities;
using TaskManagementSystem.BLL.Models;

namespace TaskManagementSystem.BLL.Services;

public class AuthService(IJwtService jwtTokenProvider,
                        UserManager<ApplicationUser> userManager
                        ) : IAuthService
{
    public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email!);

        if (user == null || !await userManager.CheckPasswordAsync(user, request.Password!))
        {
            return Result<AuthResponse>.Failure("Invalid email or password.");
        }

        var token = jwtTokenProvider.GenerateTokenAsync(new UserValues
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName
        });

        return Result<AuthResponse>.Success(
            new AuthResponse
            {
                Email = user.Email,
                TokenInfo = token
            }
            );
    }

    public Task LogoutAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request)
    {
        if (request.Password != request.ConfirmPassword)
        {
            return Result<AuthResponse>.Failure("Password and confirmation password do not match.");
        }

        var existingUser = await userManager.FindByEmailAsync(request.Email!);
        if (existingUser != null)
        {
            return Result<AuthResponse>.Failure("An account with this email already exists.");
        }

        var user = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.Email,
            FullName = request.FullName!
        };

        var result1 = await userManager.CreateAsync(user, request.Password!);


        if (!result1.Succeeded)
        {
            var errors = string.Join(", ", result1.Errors.Select(e => e.Description));
            return Result<AuthResponse>.Failure($"Registration failed: {errors}");
        }

        var token = jwtTokenProvider.GenerateTokenAsync(new UserValues
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
        });

        return Result<AuthResponse>.Success(
            new AuthResponse
            {
                Email = user.Email,
                TokenInfo = token
            }
            );
    }
}