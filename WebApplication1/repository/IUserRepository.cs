using WebApplication1.model;

namespace WebApplication1.repository;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAllUsers();

    public Task<User?> GetUser(Guid id);
    
    public Task<User?> GetUserByEmail(string email);

    public Task <User>CreateUser(User user);

    public Task UpdateUser(User user);

    public Task DeleteUser(Guid id);
}