using WebApplication1.auth;
using WebApplication1.model;
using WebApplication1.repository;

namespace WebApplication1.setvice;

public class UserService : IUserService
{
    private readonly IJwtProvaider _jwtProvaider;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly RabbitMqService _rabbitMqService;

    public UserService(IJwtProvaider jwtProvaider, IPasswordHasher passwordHasher, IUserRepository userRepository, RabbitMqService rabbitMqService)
    {
        _jwtProvaider = jwtProvaider;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _rabbitMqService = rabbitMqService;
    }

    public async Task Register(string username, string email, string password)
    {
        var hashedPassword = _passwordHasher.Generate(password);

        var user = User.Create(Guid.NewGuid(), username, hashedPassword, email);

        await _userRepository.CreateUser(user);
        
        _rabbitMqService.Publish(user);
        
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _userRepository.GetUserByEmail(email);

        if (user == null)
        {
            throw new Exception($"User with email {email} not found.");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new Exception("Password not found for the user.");
        }

        var result = _passwordHasher.Verify(password, user.Password);
        if (result == false)
        {
            throw new Exception("Failed to login");
        }

        var token = _jwtProvaider.GenerateToken(user);
        return token;
    }
}