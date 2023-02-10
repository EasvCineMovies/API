using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Controller = DevOpsCineMovies.Controllers.Controller;

namespace Tests;

public class Tests
{
    [Test]
    public async Task GreetTest()
    {
        var body = new Dictionary<string, string>
        {
            { "greeting", "Hello World!" }
        };

        var json = JsonConvert.SerializeObject(body);
        var request = new DefaultHttpContext
        {
            Request =
            {
                Body = new MemoryStream(Encoding.UTF8.GetBytes(json))
            }
        };
        var controller = new Controller
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = request
            }
        };
        var response = await controller.Greet();
        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.TypeOf<string>());
        Console.WriteLine(response);
    }
}