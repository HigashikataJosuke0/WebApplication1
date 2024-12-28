using WebApplication1.model;

namespace WebApplication1.repository;

public interface IHabitsRepository
{
    public Task<IEnumerable<Habits>> GetAllHabits();

    public Task<Habits?> GetHabits(Guid id);

    public Task <Habits>CreateHabits(Habits habits);

    public Task UpdateHabits(Habits habits);

    public Task DeleteHabits(Guid id);
}