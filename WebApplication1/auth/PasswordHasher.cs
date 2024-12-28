namespace WebApplication1.setvice;

public class PasswordHasher : IPasswordHasher
{
 
    /// <summary>
    /// Хэширует пароль
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public string Generate(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    /// <summary>
    /// Метод подтверждает что введенный пароль соответствует хэшированному
    /// </summary>
    /// <param name="password"></param>
    /// <param name="hashedPassword"></param>
    /// <returns></returns>
    public bool Verify(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
}