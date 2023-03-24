using DevOpsCineMovies.Entities;

namespace DevOpsCineMovies.Requests;

public abstract class ReservationRequest
{
    public static Reservation Create(dynamic body)
    {
        return new Reservation
        {
            UserPhone = body.userPhone,
            SeatId = body.seatId,
            MovieId = body.movieId,
            CinemaId = body.cinemaId,
            ScheduleId = body.scheduleId,
            ReservationDate = body.reservationDate,
            Price = body.price
        };
    }

    public static Reservation Read(dynamic body)
    {
        return new Reservation
        {
            Id = body.id
        };
    }

    public static Reservation Update(dynamic body)
    {
        return new Reservation
        {
            Id = body.id,
            UserPhone = body.userPhone,
            SeatId = body.seatId,
            MovieId = body.movieId,
            CinemaId = body.cinemaId,
            ScheduleId = body.scheduleId,
            ReservationDate = body.reservationDate,
            Price = body.price
        };
    }

    public static Reservation Delete(dynamic body)
    {
        return new Reservation
        {
            Id = body.id
        };
    }
}