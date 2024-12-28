namespace WebApplication1.model;

public class BadHabits : Habits
{
    /// <summary>
    /// Последствия или штраф за выполнение плохой привычки.
    /// </summary>
    public string? Penalty { get; set; }
}