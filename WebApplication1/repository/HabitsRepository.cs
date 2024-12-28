using Microsoft.EntityFrameworkCore;
using WebApplication1.model;

namespace WebApplication1.repository;

public class HabitsRepository : IHabitsRepository
{
    private readonly ApplicationDbContext _context;
    
    public HabitsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Habits>> GetAllHabits() =>
        await _context.Habits.ToListAsync();


    public async Task<Habits?> GetHabits(Guid id) =>
        await _context.Habits.FirstOrDefaultAsync(h => h.Id == id);
    
    
    public async Task<Habits> CreateHabits(Habits habits)
    {
        await _context.Habits.AddAsync(habits);
        await _context.SaveChangesAsync();
        return habits;
    }

    public async Task UpdateHabits(Habits habits)
    {
        _context.Habits.Update(habits);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteHabits(Guid id)
    {
        var habits = await _context.Habits.FindAsync(id); 
        if (habits != null)
        {
            _context.Habits.Remove(habits); 
            await _context.SaveChangesAsync(); 
        }
    }
}