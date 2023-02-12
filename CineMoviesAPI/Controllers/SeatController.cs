using DevOpsCineMovies.Context;
using DevOpsCineMovies.Entities;
using DevOpsCineMovies.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevOpsCineMovies.Controllers;

[ApiController]
[Route("[controller]")]
public class SeatController : ControllerBase
{
    private readonly MyDbContext _context = new();

    [HttpPost]
    [Route(nameof(Create))]
    public async Task<object> Create()
    {
        var response = await Validator.Body<Seat>(Request.Body, Method.Create);
        if (response is not Seat seat)
            return response;

        _context.Seats.Add(seat);
        await _context.SaveChangesAsync();

        return CustomResponse.Create("success", "Seat created", seat);
    }

    [HttpPost]
    [Route(nameof(Read))]
    public async Task<object> Read()
    {
        var response = await Validator.Body<Seat>(Request.Body, Method.Read);
        if (response is not Seat seat)
            return response;

        return await _context.Seats.FindAsync(seat.Id) is { } foundSeat
            ? CustomResponse.Create("success", "Seat found", foundSeat)
            : CustomResponse.Create("error", "Seat not found");
    }

    [HttpPost]
    [Route(nameof(Update))]
    public async Task<object> Update()
    {
        var response = await Validator.Body<Seat>(Request.Body, Method.Update);
        if (response is not Seat seat)
            return response;

        var dbSeat = await _context.Seats.FindAsync(seat.Id);
        if (dbSeat is null)
            return CustomResponse.Create("error", "Seat not found");

        dbSeat.Row = seat.Row;
        dbSeat.Column = seat.Column;
        dbSeat.CinemaId = seat.CinemaId;
        await _context.SaveChangesAsync();

        return CustomResponse.Create("success", "Seat updated", dbSeat);
    }

    [HttpPost]
    [Route(nameof(Delete))]
    public async Task<object> Delete()
    {
        var response = await Validator.Body<Seat>(Request.Body, Method.Delete);
        if (response is not Seat seat)
            return response;

        var dbSeat = await _context.Seats.FindAsync(seat.Id);
        if (dbSeat is null)
            return CustomResponse.Create("error", "Seat not found");

        _context.Seats.Remove(dbSeat);
        await _context.SaveChangesAsync();

        return CustomResponse.Create("success", "Seat deleted", dbSeat);
    }
}