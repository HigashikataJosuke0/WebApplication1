using Microsoft.EntityFrameworkCore;
using WebApplication1.model;
using WebApplication1.setvice;

namespace WebApplication1.repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public UserRepository(ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<IEnumerable<User>> GetAllUsers() => await _context.User.Include(u => u.Habits).ToListAsync();


    public async Task<User?> GetUser(Guid id) =>
        await _context.User.Include(u => u.Habits).FirstOrDefaultAsync(u => u.Id == id);


    public async Task<User> CreateUser(User user)
    {
        await _context.User.AddAsync(user);

        // if (user.Habits != null)
        // {
        //     foreach (var habit in user.Habits)
        //     {
        //         habit.UserId = user.Id;
        //         _context.Habits.Add(habit);
        //     }
        // }

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> GetUserByEmail(string email) =>
        await _context.User.FirstOrDefaultAsync(u => u.Email == email);

    public async Task UpdateUser(User user)
    {
        _context.User.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUser(Guid id)
    {
        var user = await _context.User.FindAsync(id);
        if (user != null)
        {
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
    
    
}