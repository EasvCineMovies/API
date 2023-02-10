using DevOpsCineMovies.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DevOpsCineMovies.Controllers;

[ApiController]
[Route("[controller]")]
public class Controller : ControllerBase
{
    private readonly BodyHandler _bodyHandler = new();
    
    /// <summary>
    ///     Basic greeting method.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route(nameof(Greet))]
    public async Task<string> Greet()
    {
        var body = await _bodyHandler.Get(Request.Body);

        var greeting = body.greeting;
        
        return greeting;
    }
}