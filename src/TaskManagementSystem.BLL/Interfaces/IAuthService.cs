using TaskManagementSystem.BLL.DTOs;
using TaskManagementSystem.DAL.Common;

namespace TaskManagementSystem.BLL.Interfaces;

public interface IAuthService
{
    Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request);
    Task<Result<AuthResponse>> LoginAsync(LoginRequest request);
    Task LogoutAsync();
}

