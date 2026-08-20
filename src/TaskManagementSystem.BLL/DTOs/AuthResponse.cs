using TaskManagementSystem.BLL.Models;

namespace TaskManagementSystem.BLL.DTOs;
public class AuthResponse
{
    public string? Email { get; set; }
    public Token? TokenInfo { get; set; }
}   