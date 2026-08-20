using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.BLL.DTOs;

public class RegisterRequest
{
    [Required, EmailAddress]
    public string? Email { get; set; }

    [Required, MinLength(8)]
    public string? Password { get; set; }

    [Required, Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
    public string? ConfirmPassword { get; set; }

    [Required, MaxLength(100)]
    public string? FullName { get; set; }
}