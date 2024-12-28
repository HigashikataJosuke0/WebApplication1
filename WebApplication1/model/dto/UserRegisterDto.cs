namespace WebApplication1.model.dto;

public class UserRegisterDto
{
    public required string Username { get; set; }

    public required string Email { get; set; }
    
    public required string Password { get; set; }
}