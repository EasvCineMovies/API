using System.Globalization;
using System.Text;
using DevOpsCineMovies.Context;
using DevOpsCineMovies.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tests;

internal class ReservationTests
{
    private readonly MyDbContext _context = new();
    private int _id;
    private int _minCinemaId;
    private int _minMovieId;
    private int _minScheduleId;
    private int _minSeatId;

    private string _userPhone;

    [SetUp]
    public void Setup()
    {
        _id = _context.Reservations.Max(s => s.Id) ?? 0;
        _userPhone = _context.Users.First().Phone;
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
            { "userPhone", _userPhone },
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
            { "phone", "bobthephone" }
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

        var asd = await reservationController.Read();
        var r = JsonConvert.SerializeObject(asd);
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
            { "id", "1" },
            { "userPhone", _userPhone },
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