namespace DevOpsCineMovies.Entities;

public class User
{
    public string? Phone { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();
}