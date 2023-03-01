using DevOpsCineMovies.Context;
using DevOpsCineMovies.Entities;
using DevOpsCineMovies.Models;
using DevOpsCineMovies.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DevOpsCineMovies.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationController : ControllerBase
{
	private readonly MyDbContext _context = new();

	[HttpPost]
	[Route(nameof(Create))]
	public async Task<object> Create()
	{
		var response = await Validator.Body<Reservation>(Request.Body, d => ReservationRequest.Create(d));
		if (response is not Reservation reservation)
			return response;

		_context.Reservations.Add(reservation);
		await _context.SaveChangesAsync();

		return CustomResponse.Create("success", "Reservation created", reservation);
	}

	[HttpPost]
	[Route(nameof(Read))]
	public async Task<object> Read()
	{
		var response = await Validator.Body<Reservation>(Request.Body, d => ReservationRequest.Read(d));
		if (response is not Reservation reservation)
			return response;

		return await _context.Reservations.FindAsync(reservation.Id) is { } foundReservation
			? CustomResponse.Create("success", "Reservation found", foundReservation)
			: CustomResponse.Create("error", "Reservation not found");
	}

	[HttpPost]
	[Route(nameof(Update))]
	public async Task<object> Update()
	{
		var response = await Validator.Body<Reservation>(Request.Body, d => ReservationRequest.Update(d));
		if (response is not Reservation reservation)
			return response;

		var dbReservation = await _context.Reservations.FindAsync(reservation.Id);
		if (dbReservation is null)
			return CustomResponse.Create("error", "Reservation not found");

		dbReservation.UserId = reservation.UserId;
		dbReservation.SeatId = reservation.SeatId;
		dbReservation.MovieId = reservation.MovieId;
		dbReservation.CinemaId = reservation.CinemaId;
		dbReservation.ScheduleId = reservation.ScheduleId;
		dbReservation.ReservationDate = reservation.ReservationDate;
		dbReservation.Price = reservation.Price;

		await _context.SaveChangesAsync();

		return CustomResponse.Create("success", "Reservation updated", dbReservation);
	}

	[HttpPost]
	[Route(nameof(Delete))]
	public async Task<object> Delete()
	{
		var response = await Validator.Body<Reservation>(Request.Body, d => ReservationRequest.Delete(d));
		if (response is not Reservation reservation)
			return response;

		var dbReservation = await _context.Reservations.FindAsync(reservation.Id);
		if (dbReservation is null)
			return CustomResponse.Create("error", "Reservation not found");

		_context.Reservations.Remove(dbReservation);
		await _context.SaveChangesAsync();

		return CustomResponse.Create("success", "Reservation deleted", dbReservation);
	}
}