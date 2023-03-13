using DevOpsCineMovies.Context;
using DevOpsCineMovies.Entities;
using DevOpsCineMovies.Models;
using DevOpsCineMovies.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DevOpsCineMovies.Controllers;

/// <summary>
///     Movie controller by Luca the guy with the smallest *hehe* cock
/// </summary>
[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly MyDbContext _context = new();

    [HttpPost]
    [Route(nameof(Create))]
    public async Task<object> Create()
    {
        var response = await Validator.Body<Movie>(Request.Body, d => MovieRequest.Create(d));
        if (response is not Movie movie)
            return response;

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return CustomResponse.Create("success", "Movie created", movie);
    }

    [HttpPost]
    [Route(nameof(Read))]
    public async Task<object> Read()
    {
        var response = await Validator.Body<Movie>(Request.Body, d => MovieRequest.Read(d));
        if (response is not Movie movie)
            return response;

        return await _context.Movies.FindAsync(movie.Id) is { } foundMovie
            ? CustomResponse.Create("success", "Movie found", foundMovie)
            : CustomResponse.Create("error", "Movie not found");
    }
    
    [HttpPost]
    [Route(nameof(ReadAll))]
    public async Task<object> ReadAll()
    {
        var response = await Validator.Body<Cinema>(Request.Body, d => CinemaRequest.Read(d));
        if (response is not Cinema cinema)
            return response;

        var foundCinema = await _context.Cinemas.FindAsync(cinema.Id);
        if (foundCinema is null)
            return CustomResponse.Create("error", "Cinema not found");

        var movies = from m in _context.Movies
            where m.CinemaId == foundCinema.Id
            select Sanitizer.RemoveVirtual(m);

        return CustomResponse.Create("success", $"Movies for cinema {foundCinema.Id} read", movies); 
    }

    [HttpPost]
    [Route(nameof(Update))]
    public async Task<object> Update()
    {
        var response = await Validator.Body<Movie>(Request.Body, d => MovieRequest.Update(d));
        if (response is not Movie movie)
            return response;

        var dbMovie = await _context.Movies.FindAsync(movie.Id);
        if (dbMovie is null)
            return CustomResponse.Create("error", "Movie not found");

        dbMovie.Name = movie.Name;
        dbMovie.Description = movie.Description;
        dbMovie.Duration = movie.Duration;
        dbMovie.Genre = movie.Genre;
        dbMovie.CinemaId = movie.CinemaId;
        await _context.SaveChangesAsync();

        return CustomResponse.Create("success", "Movie updated", dbMovie);
    }

    [HttpPost]
    [Route(nameof(Delete))]
    public async Task<object> Delete()
    {
        var response = await Validator.Body<Movie>(Request.Body, d => MovieRequest.Delete(d));
        if (response is not Movie movie)
            return response;

        var dbMovie = await _context.Movies.FindAsync(movie.Id);
        if (dbMovie is null)
            return CustomResponse.Create("error", "Movie not found");

        _context.Movies.Remove(dbMovie);
        await _context.SaveChangesAsync();

        return CustomResponse.Create("success", "Movie deleted", dbMovie);
    }
}