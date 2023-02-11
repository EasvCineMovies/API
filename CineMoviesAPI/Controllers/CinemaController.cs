using DevOpsCineMovies.Context;
using DevOpsCineMovies.Entities;
using DevOpsCineMovies.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevOpsCineMovies.Controllers;

[ApiController]
[Route("[controller]")]
public class CinemaController : ControllerBase
{
    private readonly MyDbContext _context = new();
    
    [HttpPost]
    [Route(nameof(Create))]
    public async Task<Object> Create()
    {
        var response = await Validator.Body<Cinema>(Request.Body, Method.Create);
        if (response is not Cinema cinema)
            return response;
        
        _context.Cinemas.Add(cinema);
        await _context.SaveChangesAsync();
        
        return CustomResponse.Create("success", "Cinema created", cinema);
    }
    
    [HttpPost]
    [Route(nameof(Read))]
    public async Task<Object> Read()
    {
        var response = await Validator.Body<Cinema>(Request.Body, Method.Read);
        if (response is not Cinema cinema)
            return response;
        
        return await _context.Cinemas.FindAsync(cinema.Id) is { } foundCinema
            ? CustomResponse.Create("success", "Cinema found", foundCinema)
            : CustomResponse.Create("error", "Cinema not found");
    }
    
    [HttpPost]
    [Route(nameof(Update))]
    public async Task<Object> Update()
    {
        var response = await Validator.Body<Cinema>(Request.Body, Method.Update);
        if (response is not Cinema cinema)
            return response;
        
        var dbCinema = await _context.Cinemas.FindAsync(cinema.Id);
        if (dbCinema is null)
            return CustomResponse.Create("error", "Cinema not found");
        
        dbCinema.Id = cinema.Id;
        dbCinema.Name = cinema.Name;
        dbCinema.Address = cinema.Address;
        await _context.SaveChangesAsync();
        
        return CustomResponse.Create("success", "Cinema updated", dbCinema);
    }
    
    [HttpPost]
    [Route(nameof(Delete))]
    public async Task<Object> Delete()
    {
        var response = await Validator.Body<Cinema>(Request.Body, Method.Delete);
        if (response is not Cinema cinema)
            return response;
        
        var dbCinema = await _context.Cinemas.FindAsync(cinema.Id);
        if (dbCinema is null)
            return CustomResponse.Create("error", "Cinema not found");
        
        _context.Cinemas.Remove(dbCinema);
        await _context.SaveChangesAsync();
        
        return CustomResponse.Create("success", "Cinema deleted");
    }
}