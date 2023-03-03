using DevOpsCineMovies.Context;
using DevOpsCineMovies.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests;

internal class ScheduleTests
{
	private readonly MyDbContext _context = new();
	private int _id;

	[SetUp]
	public void Setup()
	{
		_id = _context.Schedules.Max(s => s.Id) ?? 0;
	}

	[Test]
	public async Task Create()
	{
		var body = new Dictionary<string, string>
		{
			{ "cinemaId", "1" },
			{ "movieId", "1" },
			{ "fromTime", "2000-01-01 10:00:00" },
			{ "toTime", "2000-01-01 11:00:00" }
		};

		var json = JsonConvert.SerializeObject(body);
		var request = new DefaultHttpContext
		{
			Request =
			{
				Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
			}
		};
		var scheduleController = new ScheduleController
		{
			ControllerContext = new ControllerContext
			{
				HttpContext = request
			}
		};

		var r = JsonConvert.SerializeObject(await scheduleController.Create());
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
			{ "id", "1" }
		};

		var json = JsonConvert.SerializeObject(body);
		var request = new DefaultHttpContext
		{
			Request =
			{
				Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
			}
		};
		var scheduleController = new ScheduleController
		{
			ControllerContext = new ControllerContext
			{
				HttpContext = request
			}
		};

		var r = JsonConvert.SerializeObject(await scheduleController.Read());
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
			{ "cinemaId", "1" },
			{ "movieId", "1" },
			{ "fromTime", "2000-01-01 11:00:00" },
			{ "toTime", "2000-01-01 12:00:00" }
		};

		var json = JsonConvert.SerializeObject(body);
		var request = new DefaultHttpContext
		{
			Request =
			{
				Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
			}
		};
		var scheduleController = new ScheduleController
		{
			ControllerContext = new ControllerContext
			{
				HttpContext = request
			}
		};

		var r = JsonConvert.SerializeObject(await scheduleController.Update());
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
		var scheduleController = new ScheduleController
		{
			ControllerContext = new ControllerContext
			{
				HttpContext = request
			}
		};

		var r = JsonConvert.SerializeObject(await scheduleController.Delete());
		var response = JObject.Parse(r);

		Assert.That(response, Is.Not.Null);
		Assert.That(response, Is.Not.Empty);
		Assert.That(response["status"]!.ToString() == "success", Is.True);
	}
}
