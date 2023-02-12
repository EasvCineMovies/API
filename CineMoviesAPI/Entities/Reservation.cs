namespace DevOpsCineMovies.Entities;

public class Reservation
{
    public int? Id { get; set; }

    public int? UserId { get; set; }

    public int? SeatId { get; set; }

    public int? MovieId { get; set; }

    public int? CinemaId { get; set; }

    public int? ScheduleId { get; set; }

    public DateTime? ReservationDate { get; set; }

    public decimal? Price { get; set; }

    public virtual Cinema? Cinema { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual Schedule? Schedule { get; set; }

    public virtual Seat? Seat { get; set; }

    public virtual User? User { get; set; }
}