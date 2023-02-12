using DevOpsCineMovies.Entities;

namespace DevOpsCineMovies.Requests;

public abstract class SeatRequest
{
    public static Seat Create(dynamic body)
    {
        return new Seat
        {
            Row = body.row,
            Column = body.column,
            CinemaId = body.cinemaId
        };
    }

    public static Seat Read(dynamic body)
    {
        return new Seat
        {
            Id = body.id
        };
    }

    public static Seat Update(dynamic body)
    {
        return new Seat
        {
            Id = body.id,
            Row = body.row,
            Column = body.column,
            CinemaId = body.cinemaId
        };
    }

    public static Seat Delete(dynamic body)
    {
        return new Seat
        {
            Id = body.id
        };
    }
}