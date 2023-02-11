using DevOpsCineMovies.Entities;

namespace DevOpsCineMovies.Requests;

public abstract class MovieRequest
{
    public static Movie Create(dynamic body)
    {
        return new Movie
        {
            Name = body.name,
            CinemaId = body.cinemaId,
            Description = body.description,
            Duration = body.duration,
            Genre = body.Genre
        };
    }

    public static Movie Read(dynamic body)
    {
        return new Movie
        {
            Id = body.id
        };
    }

    public static Movie Update(dynamic body)
    {
        return new Movie
        {
            Id = body.id,
            Name = body.name,
            CinemaId = body.cinemaId,
            Description = body.description,
            Duration = body.duration,
            Genre = body.Genre
        };
    }

    public static Movie Delete(dynamic body)
    {
        return new Movie
        {
            Id = body.id
        };
    }
}