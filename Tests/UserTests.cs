using System.Text;
using DevOpsCineMovies.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tests;

public class UserTests
{
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

        var response = await userController.Create();
        Assert.That(response, Is.Not.Null);
        var responseJson = JObject.Parse(response.ToString() ?? string.Empty);
        Assert.That(responseJson, Is.Not.Empty);
        Assert.That(responseJson["status"]!.ToString() == "success", Is.True);
    }

    [Test]
    public async Task Read()
    {
        var body = new Dictionary<string, string>
        {
            { "id", "3" }
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

        var response = await userController.Read();
        Assert.That(response, Is.Not.Null);
        var responseJson = JObject.Parse(response.ToString() ?? string.Empty);
        Assert.That(responseJson, Is.Not.Empty);
        Assert.That(responseJson["status"]!.ToString() == "success", Is.True);
    }

    [Test]
    public async Task Update()
    {
        var body = new Dictionary<string, string>
        {
            { "id", "5" },
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

        var response = await userController.Update();
        Assert.That(response, Is.Not.Null);
        var responseJson = JObject.Parse(response.ToString() ?? string.Empty);
        Assert.That(responseJson, Is.Not.Empty);
        Assert.That(responseJson["status"]!.ToString() == "success", Is.True);
    }

    [Test]
    public async Task Delete()
    {
        var body = new Dictionary<string, string>
        {
            { "id", "5" }
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

        var response = await userController.Delete();
        Assert.That(response, Is.Not.Null);
        var responseJson = JObject.Parse(response.ToString() ?? string.Empty);
        Assert.That(responseJson, Is.Not.Empty);
        Assert.That(responseJson["status"]!.ToString() == "success", Is.True);
    }
}