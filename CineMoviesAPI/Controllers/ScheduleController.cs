using DevOpsCineMovies.Context;
using DevOpsCineMovies.Entities;
using DevOpsCineMovies.Models;
using DevOpsCineMovies.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevOpsCineMovies.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleController : ControllerBase
{
	private readonly MyDbContext _context = new();

	[HttpPost]
	[Route(nameof(Create))]
	public async Task<object> Create()
	{
		var response = await Validator.Body<Schedule>(Request.Body, d => ScheduleRequest.Create(d));
		if (response is not Schedule schedule)
			return response;

		_context.Schedules.Add(schedule);
		await _context.SaveChangesAsync();

		return CustomResponse.Create("success", "Schedule created", schedule);
	}

	[HttpPost]
	[Route(nameof(Read))]
	public async Task<object> Read()
	{
		var response = await Validator.Body<Cinema>(Request.Body, d => CinemaRequest.Read(d));
		if (response is not Cinema cinema)
			return response;
		
		var foundCinema = await _context.Cinemas.FindAsync(cinema.Id);
		if (foundCinema is null)
			return CustomResponse.Create("error", "Cinema not found");
		
		var schedules = from s in _context.Schedules
			where s.CinemaId == foundCinema.Id
			select Sanitizer.RemoveVirtual(s);

		return CustomResponse.Create("success", $"Schedules for cinema {foundCinema.Id} read", schedules);
	}

	[HttpPost]
	[Route(nameof(Update))]
	public async Task<object> Update()
	{
		var response = await Validator.Body<Schedule>(Request.Body, d => ScheduleRequest.Update(d));
		if (response is not Schedule schedule)
			return response;

		var dbSchedule = await _context.Schedules.FindAsync(schedule.Id);
		if (dbSchedule is null)
			return CustomResponse.Create("error", "Schedule not found");

		dbSchedule.CinemaId = schedule.CinemaId;
		dbSchedule.MovieId = schedule.MovieId;
		dbSchedule.FromTime = schedule.FromTime;
		dbSchedule.ToTime = schedule.ToTime;

		await _context.SaveChangesAsync();

		return CustomResponse.Create("success", "Schedule updated", dbSchedule);
	}

	[HttpPost]
	[Route(nameof(Delete))]
	public async Task<object> Delete()
	{
		var response = await Validator.Body<Schedule>(Request.Body, d => ScheduleRequest.Delete(d));
		if (response is not Schedule schedule)
			return response;

		var dbSchedule = await _context.Schedules.FindAsync(schedule.Id);
		if (dbSchedule is null)
			return CustomResponse.Create("error", "Schedule not found");

		_context.Schedules.Remove(dbSchedule);
		await _context.SaveChangesAsync();

		return CustomResponse.Create("success", "Schedule deleted", dbSchedule);
	}
}