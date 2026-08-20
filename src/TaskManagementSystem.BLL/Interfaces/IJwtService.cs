using TaskManagementSystem.BLL.Models;

namespace TaskManagementSystem.BLL.Interfaces;

public interface IJwtService
{    
    public Token GenerateTokenAsync(UserValues user);
}

