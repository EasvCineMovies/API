using DevOpsCineMovies.Entities;

namespace DevOpsCineMovies.Requests;

/// <summary>
///     This class is used to create different instances of the User with the necessary properties.
/// </summary>
public abstract class UserRequest
{
    public static User Create(dynamic body)
    {
        return new User
        {
            Name = body.name,
            Phone = body.phone,
            Email = body.email,
            Password = body.password
        };
    }

    public static User Read(dynamic body)
    {
        return new User
        {
            Id = body.id
        };
    }

    public static User Update(dynamic body)
    {
        return new User
        {
            Id = body.id,
            Name = body.name,
            Phone = body.phone,
            Email = body.email,
            Password = body.password
        };
    }

    public static User Delete(dynamic body)
    {
        return new User
        {
            Id = body.id
        };
    }
}