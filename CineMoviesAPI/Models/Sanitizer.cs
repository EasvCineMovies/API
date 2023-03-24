namespace DevOpsCineMovies.Models;

public abstract class Sanitizer
{
    public static T RemoveVirtual<T>(T obj)
    {
        var properties = obj!.GetType().GetProperties();

        foreach (var property in properties)
            if (property.GetMethod.IsVirtual)
                try
                {
                    property.SetValue(obj, null);
                }
                catch
                {
                }

        return obj;
    }
}