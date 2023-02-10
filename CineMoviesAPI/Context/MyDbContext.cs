using DevOpsCineMovies.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevOpsCineMovies.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cinema> Cinemas { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(
            "Server=easvcinemovies.database.windows.net;database=easvcinemovies;user id=group3@easvcinemovies;password=B1gd!ck+;trusted_connection=true;TrustServerCertificate=True;integrated security=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cinema>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cinema__3213E83F106277DE");

            entity.ToTable("cinema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__movie__3213E83F10D208EA");

            entity.ToTable("movie");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CinemaId).HasColumnName("cinemaId");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Genre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("genre");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.Cinema).WithMany(p => p.Movies)
                .HasForeignKey(d => d.CinemaId)
                .HasConstraintName("FK__movie__cinemaId__5EBF139D");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__reservat__3213E83F2937F6E3");

            entity.ToTable("reservation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CinemaId).HasColumnName("cinemaId");
            entity.Property(e => e.MovieId).HasColumnName("movieId");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ReservationDate)
                .HasColumnType("datetime")
                .HasColumnName("reservationDate");
            entity.Property(e => e.ScheduleId).HasColumnName("scheduleId");
            entity.Property(e => e.SeatId).HasColumnName("seatId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Cinema).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CinemaId)
                .HasConstraintName("FK__reservati__cinem__6D0D32F4");

            entity.HasOne(d => d.Movie).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__reservati__movie__6C190EBB");

            entity.HasOne(d => d.Schedule).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK__reservati__sched__6E01572D");

            entity.HasOne(d => d.Seat).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.SeatId)
                .HasConstraintName("FK__reservati__seatI__6B24EA82");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__reservati__userI__6A30C649");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__schedule__3213E83F43E01958");

            entity.ToTable("schedule");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CinemaId).HasColumnName("cinemaId");
            entity.Property(e => e.FromTime)
                .HasColumnType("datetime")
                .HasColumnName("fromTime");
            entity.Property(e => e.MovieId).HasColumnName("movieId");
            entity.Property(e => e.ToTime)
                .HasColumnType("datetime")
                .HasColumnName("toTime");

            entity.HasOne(d => d.Cinema).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.CinemaId)
                .HasConstraintName("FK__schedule__cinema__619B8048");

            entity.HasOne(d => d.Movie).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__schedule__movieI__628FA481");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__seat__3213E83F5641D410");

            entity.ToTable("seat");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CinemaId).HasColumnName("cinemaId");
            entity.Property(e => e.Column).HasColumnName("column");
            entity.Property(e => e.Row).HasColumnName("row");

            entity.HasOne(d => d.Cinema).WithMany(p => p.Seats)
                .HasForeignKey(d => d.CinemaId)
                .HasConstraintName("FK__seat__cinemaId__656C112C");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user__3213E83FE9B33BCB");

            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}