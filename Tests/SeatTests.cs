using System.Text;
using DevOpsCineMovies.Context;
using DevOpsCineMovies.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tests;

public class SeatTests
{
    private readonly MyDbContext _context = new();
    private int _id;

    [SetUp]
    public void Setup()
    {
        _id = _context.Seats.Max(s => s.Id) ?? 0;
    }

    [Test]
    public async Task Create()
    {
        var body = new Dictionary<string, string>
        {
            { "row", "1" },
            { "column", "1" },
            { "cinemaId", "1" }
        };

        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };
        var seatController = new SeatController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };

        var r = JsonConvert.SerializeObject(await seatController.Create());
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
            { "id", _id.ToString() }
        };

        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };
        var seatController = new SeatController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };

        var r = JsonConvert.SerializeObject(await seatController.Read());
        var response = JObject.Parse(r);

        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.Not.Empty);
        Assert.That(response["status"]!.ToString() == "success", Is.True);
    }

    [Test]
    public async Task ReadAll()
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
        var seatController = new SeatController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };

        var r = JsonConvert.SerializeObject(await seatController.ReadAll());
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
            { "id", _id.ToString() },
            { "row", "1" },
            { "column", "1" },
            { "cinemaId", "1" }
        };

        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };
        var seatController = new SeatController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };

        var r = JsonConvert.SerializeObject(await seatController.Update());
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
            { "id", _id.ToString() }
        };

        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };
        var seatController = new SeatController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };

        var r = JsonConvert.SerializeObject(await seatController.Delete());
        var response = JObject.Parse(r);

        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.Not.Empty);
        Assert.That(response["status"]!.ToString() == "success", Is.True);
    }
}