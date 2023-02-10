namespace DevOpsCineMovies.Models;

public class GreetingRequest
{
    public GreetingRequest(dynamic? body)
    {
        Greeting = body.greeting;
    }

    public string Greeting { get; }
}