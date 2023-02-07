using Microsoft.AspNetCore.Mvc;

namespace DevOpsCineMovies.Controllers;

[ApiController]
[Route("[controller]")]
public class Controller : ControllerBase
{
    /// <summary>
    ///     Basic greeting method.
    ///     Async is not required in this instance, but it is a good practice for future use.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route(nameof(Greet))]
    public async Task<string> Greet()
    {
        return "Hello World!";
    }
}