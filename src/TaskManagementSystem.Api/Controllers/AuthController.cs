using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.BLL.DTOs;
using TaskManagementSystem.BLL.Interfaces;

namespace TaskManagementSystem.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await authService.RegisterAsync(request);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        var response = result.Value;
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await authService.LoginAsync(request);

        if (!result.IsSuccess)
            return Unauthorized(new { error = result.Error });

        var response = result.Value;
        return Ok(response);
    }
}