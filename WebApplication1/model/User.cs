using WebApplication1.auth;

namespace WebApplication1.model;

public class User
{
    public User()
    {
    }

    public User(Guid id, string? username, string? lastname, string? login, string? password, string? email)
    {
        Id = id;
        Username = username;
        Lastname = lastname;
        Login = login;
        Password = password;
        Email = email;
    }

    public User(Guid id, string? username, string? password, string? email)
    {
        Id = id;
        Username = username;
        Password = password;
        Email = email;
    }

    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public string? Lastname { get; set; }

    /// <summary>
    /// Логин пользователя.
    /// </summary>
    public string? Login { get; set; }
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Роль доступа
    /// </summary>
    public Roles Role { get; set; } = Roles.User;

    /// <summary>
    /// email пользователя.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Связь с привычкой.
    /// </summary>
    public List<Habits>? Habits { get; set; }

    /// <summary>
    /// Связь с записью.
    /// </summary>
    public List<RecordOfExecution>? RecordOfExecutions { get; set; }

    /// <summary>
    /// Метод для создания юзера 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    public static User Create(Guid id, string? username, string? password, string? email)
    {
        return new User(id, username, password, email);
    }
}