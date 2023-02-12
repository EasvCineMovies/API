namespace DevOpsCineMovies.Entities;

public class Schedule
{
    public int? Id { get; set; }

    public int? CinemaId { get; set; }

    public int? MovieId { get; set; }

    public DateTime? FromTime { get; set; }

    public DateTime? ToTime { get; set; }

    public virtual Cinema? Cinema { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();
}