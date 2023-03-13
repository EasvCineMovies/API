using System.Text;
using DevOpsCineMovies.Context;
using DevOpsCineMovies.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tests;

public class UserTests
{
    private readonly MyDbContext _context = new();
    private int _id;

    [SetUp]
    public void Setup()
    {
        _id = _context.Users.Max(u => u.Id) ?? 0;
    }

    [Test]
    public async Task Create()
    {
        var body = new Dictionary<string, string>
        {
            { "name", "John Doe" },
            { "phone", "123456789" },
            { "email", "something" },
            { "password", "something" }
        };

        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };

        var userController = new UserController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };

        var r = JsonConvert.SerializeObject(await userController.Create());
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

        var userController = new UserController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };

        var r = JsonConvert.SerializeObject(await userController.Read());
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
            { "name", "John Doeeeefsde" },
            { "phone", "123456789" },
            { "email", "something" },
            { "password", "something" }
        };

        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };

        var userController = new UserController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };

        var r = JsonConvert.SerializeObject(await userController.Update());
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

        var userController = new UserController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };

        var r = JsonConvert.SerializeObject(await userController.Delete());
        var response = JObject.Parse(r);
        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.Not.Empty);
        Assert.That(response["status"]!.ToString() == "success", Is.True);
    }

    [Test]
    public async Task Login()
    {
        var body = new Dictionary<string, string>
        {
            { "phone", "bobthephone" },
            { "password", "bobthepassword" }
        };
        
        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };
        
        var userController = new UserController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };
        
        var r = JsonConvert.SerializeObject(await userController.Login());
        var response = JObject.Parse(r);
        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.Not.Empty);
        Assert.That(response["status"]!.ToString() == "success", Is.True);
    }
}