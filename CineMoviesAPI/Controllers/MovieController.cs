using DevOpsCineMovies.Context;
using DevOpsCineMovies.Entities;
using DevOpsCineMovies.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevOpsCineMovies.Controllers;
/// <summary>
/// Movie controller by Luca the guy with the biggest cock
/// </summary>
[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly MyDbContext _context = new();
    
    [HttpPost]
    [Route(nameof(Create))]
    public async Task<Object> Create()
    {
        var response = await Validator.Body<Movie>(Request.Body, Method.Create);
        if (response is not Movie movie)
            return response;
        
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
        
        return CustomResponse.Create("success", "Movie created", movie);
    }
    
    [HttpPost]
    [Route(nameof(Read))]
    public async Task<Object> Read()
    {
        var response = await Validator.Body<Movie>(Request.Body, Method.Read);
        if (response is not Movie movie)
            return response;
        
        return await _context.Movies.FindAsync(movie.Id) is { } foundMovie
            ? CustomResponse.Create("success", "Movie found", foundMovie)
            : CustomResponse.Create("error", "Movie not found");
    }
    
    [HttpPost]
    [Route(nameof(Update))]
    public async Task<Object> Update()
    {
        var response = await Validator.Body<Movie>(Request.Body, Method.Update);
        if (response is not Movie movie)
            return response;
        
        var dbMovie = await _context.Movies.FindAsync(movie.Id);
        if (dbMovie is null)
            return CustomResponse.Create("error", "Movie not found");
        
        dbMovie.Id = movie.Id;
        dbMovie.Name = movie.Name;
        dbMovie.Description = movie.Description;
        dbMovie.Duration = movie.Duration;
        dbMovie.Genre = movie.Genre;
        dbMovie.CinemaId = movie.CinemaId;
        await _context.SaveChangesAsync();
        
        return CustomResponse.Create("success", "Movie updated", movie);
    }
    
    [HttpPost]
    [Route(nameof(Delete))]
    public async Task<Object> Delete()
    {
        var response = await Validator.Body<Movie>(Request.Body, Method.Delete);
        if (response is not Movie movie)
            return response;
        
        var dbMovie = await _context.Movies.FindAsync(movie.Id);
        if (dbMovie is null)
            return CustomResponse.Create("error", "Movie not found");
        
        _context.Movies.Remove(dbMovie);
        await _context.SaveChangesAsync();
        
        return CustomResponse.Create("success", "Movie deleted");
    }
}