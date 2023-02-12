using DevOpsCineMovies.Entities;

namespace DevOpsCineMovies.Requests;

public abstract class CinemaRequest
{
    public static Cinema Create(dynamic body)
    {
        return new Cinema
        {
            Name = body.name,
            Address = body.address
        };
    }

    public static Cinema Read(dynamic body)
    {
        return new Cinema
        {
            Id = body.id
        };
    }

    public static Cinema Update(dynamic body)
    {
        return new Cinema
        {
            Id = body.id,
            Name = body.name,
            Address = body.address
        };
    }

    public static Cinema Delete(dynamic body)
    {
        return new Cinema
        {
            Id = body.id
        };
    }
}