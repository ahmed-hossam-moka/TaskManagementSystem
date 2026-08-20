using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using TaskManagementSystem.BLL.Models;
using TaskManagementSystem.BLL.Interfaces;

namespace TaskManagementSystem.BLL.Services;

public class JwtTokenProvider(IConfiguration configuration) : IJwtService
{
    public Token GenerateTokenAsync(UserValues user)
    {
        var JwtSettings = configuration.GetSection("JwtSettings");

        var issuer = JwtSettings["Issuer"]!; 
        var audience = JwtSettings["Audience"]!;
        var key = JwtSettings["SecretKey"]!;
        var expires = DateTime.UtcNow.AddMinutes(int.Parse(JwtSettings["TokenExpirationInMinutes"]!));

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.GivenName, user.FullName!),
        };


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                                        new SymmetricSecurityKey(
                                                Encoding.UTF8.GetBytes(key)),
                                                SecurityAlgorithms.HmacSha256Signature
                                        )
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        
        return new Token
        {
            AccessToken= tokenHandler.WriteToken(securityToken),
            Expirers= expires
        };
    }
}
