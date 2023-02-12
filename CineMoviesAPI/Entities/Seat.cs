namespace DevOpsCineMovies.Entities;

public class Seat
{
    public int? Id { get; set; }

    public int? CinemaId { get; set; }

    public int? Row { get; set; }

    public int? Column { get; set; }

    public virtual Cinema? Cinema { get; set; }

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();
}