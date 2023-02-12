namespace DevOpsCineMovies.Models;

public abstract class CustomResponse
{
    public static object Create(string status, string message, object? data = null)
    {
        var notNullData = data ?? new object();
        var response = new Dictionary<string, object>
        {
            { "status", status },
            { "message", message },
            { "data", notNullData }
        };
        return response;
    }
}