namespace DevOpsCineMovies.Models;

public class GreetingRequest
{

    public string Greeting { get; }

    public GreetingRequest(dynamic? body)
    {
        Greeting = body.greeting;
    }

}