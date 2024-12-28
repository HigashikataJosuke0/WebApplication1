namespace WebApplication1.setvice;

public interface IPasswordHasher
{
    string Generate(string password);

    bool Verify(string password, string hashedPassword);
}