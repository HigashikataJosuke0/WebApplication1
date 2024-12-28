namespace WebApplication1.model;


public abstract class Habits
{
    /// <summary>
    /// Уникальный идентификатор привычки
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название привычки
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Описание привычки
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Внешний ключ на user 
    /// </summary>
    public Guid UserId { get; set; }
    
    
}