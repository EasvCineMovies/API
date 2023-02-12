using DevOpsCineMovies.Context;
using DevOpsCineMovies.Controllers;
using DevOpsCineMovies.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests;

internal class ReservationTests
{
	private readonly MyDbContext _context = new();
	private int _id;
	
	private int _minUserId;
	private int _minSeatId;
	private int _minMovieId;
	private int _minCinemaId;
	private int _minScheduleId;
	
	[SetUp]
	public void Setup()
	{
		_id = _context.Reservations.Max(s => s.Id) ?? 0;
		_minUserId = _context.Users.Min(s => s.Id) ?? 0;
		_minSeatId = _context.Seats.Min(s => s.Id) ?? 0;
		_minMovieId = _context.Movies.Min(s => s.Id) ?? 0;
		_minCinemaId = _context.Cinemas.Min(s => s.Id) ?? 0;
		_minScheduleId = _context.Schedules.Min(s => s.Id) ?? 0;
	}

	[Test]
	public async Task Create()
	{
		var body = new Dictionary<string, string>
		{
			{ "userId", _minUserId.ToString() },
			{ "seatId", _minSeatId.ToString() },
			{ "movieId", _minMovieId.ToString() },
			{ "cinemaId", _minCinemaId.ToString() },
			{ "scheduleId", _minScheduleId.ToString() },
			{ "reservationDate", DateTime.Now.ToString(CultureInfo.InvariantCulture) },
			{ "price", "499" }
		};

		var json = JsonConvert.SerializeObject(body);
		var request = new DefaultHttpContext
		{
			Request =
			{
				Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
			}
		};
		var reservationController = new ReservationController
		{
			ControllerContext = new ControllerContext
			{
				HttpContext = request
			}
		};

		var r = JsonConvert.SerializeObject(await reservationController.Create());
		var response = JObject.Parse(r);

		Assert.That(response, Is.Not.Null);
		Assert.That(response, Is.Not.Empty);
		Assert.That(response["status"]!.ToString() == "success", Is.True);
	}

	[Test]
	public async Task Read()
	{
		var body = new Dictionary<string, string>
		{
			{ "id", (_id-1).ToString() }
		};

		var json = JsonConvert.SerializeObject(body);
		var request = new DefaultHttpContext
		{
			Request =
			{
				Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
			}
		};
		var reservationController = new ReservationController
		{
			ControllerContext = new ControllerContext
			{
				HttpContext = request
			}
		};

		var r = JsonConvert.SerializeObject(await reservationController.Read());
		var response = JObject.Parse(r);

		Assert.That(response, Is.Not.Null);
		Assert.That(response, Is.Not.Empty);
		Assert.That(response["status"]!.ToString() == "success", Is.True);
	}

	[Test]
	public async Task Update()
	{
		var body = new Dictionary<string, string>
		{
			{ "id", (_id-2).ToString() },
			{ "userId", _minUserId.ToString() },
			{ "seatId", _minSeatId.ToString() },
			{ "movieId", _minMovieId.ToString() },
			{ "cinemaId", _minCinemaId.ToString() },
			{ "scheduleId", _minScheduleId.ToString() },
			{ "reservationDate", DateTime.Now.ToString(CultureInfo.InvariantCulture) },
			{ "price", "499" }
		};

		var json = JsonConvert.SerializeObject(body);
		var request = new DefaultHttpContext
		{
			Request =
			{
				Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
			}
		};
		var reservationController = new ReservationController
		{
			ControllerContext = new ControllerContext
			{
				HttpContext = request
			}
		};

		var r = JsonConvert.SerializeObject(await reservationController.Update());
		var response = JObject.Parse(r);

		Assert.That(response, Is.Not.Null);
		Assert.That(response, Is.Not.Empty);
		Assert.That(response["status"]!.ToString() == "success", Is.True);
	}

	[Test]
	public async Task Delete()
	{
		var body = new Dictionary<string, string>
		{
			{ "id", (_id-3).ToString() }
		};

		var json = JsonConvert.SerializeObject(body);
		var request = new DefaultHttpContext
		{
			Request =
			{
				Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
			}
		};
		var reservationController = new ReservationController
		{
			ControllerContext = new ControllerContext
			{
				HttpContext = request
			}
		};

		var r = JsonConvert.SerializeObject(await reservationController.Delete());
		var response = JObject.Parse(r);

		Assert.That(response, Is.Not.Null);
		Assert.That(response, Is.Not.Empty);
		Assert.That(response["status"]!.ToString() == "success", Is.True);
	}

}
