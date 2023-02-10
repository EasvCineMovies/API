using DevOpsCineMovies.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DevOpsCineMovies.Controllers;

[ApiController]
[Route("[controller]")]
public class Controller : ControllerBase
{
    /// <summary>
    ///     Basic greeting method.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route(nameof(Greet))]
    public async Task<string> Greet()
    {
        var body = await BodyHandler.Get(Request.Body);

        var greetingRequest = new GreetingRequest(body);

        return JsonConvert.SerializeObject(greetingRequest.Greeting);
    }
}