namespace DevOpsCineMovies.Models;

public class GreetingRequest
{
    
    private string _greeting;
    
    public GreetingRequest(dynamic? body)
    {
        _greeting = body.greeting;
    }

    public string GetGreeting()
    {
        return _greeting;
    }
}