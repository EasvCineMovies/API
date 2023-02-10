using Newtonsoft.Json.Linq;

namespace DevOpsCineMovies.Models;

public abstract class CustomResponse
{
    public static JObject Create(string status, string message, dynamic? data = null)
    {
        var response = new JObject
        {
            ["status"] = status,
            ["message"] = message
        };

        if (data != null) response["data"] = JToken.FromObject(data);

        return response;
    }
}