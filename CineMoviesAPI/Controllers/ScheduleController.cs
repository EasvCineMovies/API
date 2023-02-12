using DevOpsCineMovies.Context;
using DevOpsCineMovies.Entities;
using DevOpsCineMovies.Models;
using Microsoft.AspNetCore.Mvc;

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
		var response = await Validator.Body<Schedule>(Request.Body, Method.Create);
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
		var response = await Validator.Body<Schedule>(Request.Body, Method.Read);
		if (response is not Schedule schedule)
			return response;

		return await _context.Schedules.FindAsync(schedule.Id) is { } foundSchedule
			? CustomResponse.Create("success", "Schedule found", foundSchedule)
			: CustomResponse.Create("error", "Schedule not found");
	}

	[HttpPost]
	[Route(nameof(Update))]
	public async Task<object> Update()
	{
		var response = await Validator.Body<Schedule>(Request.Body, Method.Update);
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
		var response = await Validator.Body<Schedule>(Request.Body, Method.Delete);
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