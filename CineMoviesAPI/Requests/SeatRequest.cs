using DevOpsCineMovies.Entities;

namespace DevOpsCineMovies.Requests;

public abstract class SeatRequest
{
    public static Seat Create(dynamic body)
    {
        return new Seat
        {
            CinemaId = body.cinemaId,
            Row = body.row,
            Column = body.column
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
            CinemaId = body.cinemaId,
            Row = body.row,
            Column = body.column
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