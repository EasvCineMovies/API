using Newtonsoft.Json;

namespace DevOpsCineMovies.Models;

public class BodyHandler
{
    public static async Task<dynamic?> Get(Stream stream)
    {
        var requestJson = await new StreamReader(stream).ReadToEndAsync();
        return JsonConvert.DeserializeObject(requestJson);
    }
}