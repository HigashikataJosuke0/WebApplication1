namespace WebApplication1.model;

public class RecordOfExecution
{
     /// <summary>
     /// Уникальный идентификатор записи.
     /// </summary>
     public Guid Id { get; set; }

     /// <summary>
     /// Время выполнения привычки.
     /// </summary>
     public DateTime? CompletionTime { get; set; }

     /// <summary>
     /// Фактическое значение выполнения (например, 30 минут, 10 повторений).
     /// </summary>
     public int? ActualValue { get; set; }

     /// <summary>
     /// Связь с привычкой.
     /// </summary>
     public Habits? Habits { get; set; }
     /// <summary>
     /// PK на привычки
     /// </summary>
     public Guid? HabitsId { get; set; }
     
     /// <summary>
     /// Ссылка на пользователя.
     /// </summary>
     public Guid? UserId { get; set; }
     
}