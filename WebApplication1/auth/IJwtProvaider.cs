using WebApplication1.model;

namespace WebApplication1.auth;

public interface IJwtProvaider
{
    public string GenerateToken(User user);
    
}