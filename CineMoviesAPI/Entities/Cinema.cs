namespace DevOpsCineMovies.Entities;

public class Cinema
{
    public int? Id { get; set; }

    public string? Name { get; set; } = null!;

    public string? Address { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; } = new List<Movie>();

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();

    public virtual ICollection<Schedule> Schedules { get; } = new List<Schedule>();

    public virtual ICollection<Seat> Seats { get; } = new List<Seat>();
}