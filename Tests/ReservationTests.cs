using DevOpsCineMovies.Context;
using DevOpsCineMovies.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests;

internal class ReservationTests
{
	private readonly MyDbContext _context = new();
	private int _id;

	[SetUp]
	public void Setup()
	{
		_id = _context.Reservations.Max(s => s.Id) ?? 0;
	}

	[Test]
	public async Task Create()
	{
		var body = new Dictionary<string, string>
		{
			{ "userId", "1" },
			{ "seatId", "1" },
			{ "movieId", "1" },
			{ "cinemaId", "1" },
			{ "scheduleId", "1" },
			{ "reservationDate", "2000-01-01 10:00:00" },
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
			{ "userId", "1" },
			{ "seatId", "1" },
			{ "movieId", "1" },
			{ "cinemaId", "1" },
			{ "scheduleId", "1" },
			{ "reservationDate", "2000-01-01 10:00:00" },
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
