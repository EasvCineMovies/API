namespace DevOpsCineMovies.Models;

public abstract class PasswordHelper
{
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public static bool ComparePassword(string enteredPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(enteredPassword, hashedPassword);
    }
}