namespace DevOpsCineMovies.Entities;

public class Movie
{
    public int? Id { get; set; }

    public int? CinemaId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? Duration { get; set; }

    public string? Genre { get; set; }

    public virtual Cinema? Cinema { get; set; }

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();

    public virtual ICollection<Schedule> Schedules { get; } = new List<Schedule>();
}