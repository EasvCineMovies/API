using DevOpsCineMovies.Entities;
using DevOpsCineMovies.Requests;

namespace DevOpsCineMovies.Models;

/// <summary>
///     This class is used to validate the request body.
/// </summary>
public abstract class Validator
{
    /// <summary>
    ///     This specific method is used to validate the properties of the entity that is being created, read, updated or
    ///     deleted.
    ///     If the method is Create, the Id property must be null and the other properties must not be null.
    ///     If the method is Read, the Id property must not be null and the other properties must be null.
    ///     If the method is Update, none of the properties must be null.
    ///     If the method is Delete, the Id property must not be null and the other properties must be null.
    /// </summary>
    /// <param name="obj">
    ///     The entity that is being created, read, updated or deleted.
    /// </param>
    /// <param name="method">
    ///     The method that is being used to perform the operation, options are:
    ///     Method.Create,
    ///     Method.Read,
    ///     Method.Update,
    ///     Method.Delete
    /// </param>
    /// <typeparam name="T">
    ///     The type of the entity that is being created, read, updated or deleted.
    /// </typeparam>
    /// <returns>
    ///     True if the properties are valid, otherwise false.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private static bool Properties<T>(T obj, Method method)
    {
        var properties = obj.GetType().GetProperties();

        var result = true;
        foreach (var property in properties)
            if (property.Name == "Id")
                switch (method)
                {
                    case Method.Create:
                        if (property.GetValue(obj) == null) continue;
                        result = false;
                        break;
                    case Method.Read:
                        if (property.GetValue(obj) != null) continue;
                        result = false;
                        break;
                    case Method.Update:
                        if (property.GetValue(obj) != null) continue;
                        result = false;
                        break;
                    case Method.Delete:
                        if (property.GetValue(obj) != null) continue;
                        result = false;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(method), method, null);
                }
            else if (property.GetType() == typeof(IEnumerable<>))
                switch (method)
                {
                    case Method.Create:
                        if (property.GetValue(obj) != null) continue;
                        result = false;
                        break;
                    case Method.Read:
                        if (property.GetValue(obj) == null) continue;
                        result = false;
                        break;
                    case Method.Update:
                        if (property.GetValue(obj) != null) continue;
                        result = false;
                        break;
                    case Method.Delete:
                        if (property.GetValue(obj) == null) continue;
                        result = false;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(method), method, null);
                }

        return result;
    }

    /// <summary>
    ///     This method is used to validate the request body.
    ///     If any errors occur, a CustomResponse object is returned with the error message.
    /// </summary>
    /// <param name="requestBody">
    ///     The request body that is being validated.
    /// </param>
    /// <param name="method">
    ///     The method that is being used to perform the operation, options are:
    ///     Method.Create,
    ///     Method.Read,
    ///     Method.Update,
    ///     Method.Delete
    /// </param>
    /// <typeparam name="T">
    ///     The type of the entity that we expect to operate on.
    /// </typeparam>
    /// <returns>
    ///     The entity that is being created, read, updated or deleted, or a CustomResponse object with the error message.
    /// </returns>
    public static async Task<object> Body<T>(Stream requestBody, Method method)
    {
        var body = await BodyHandler.Get(requestBody);
        if (body is null)
            return CustomResponse.Create("error", "Body is null");


        T value;
        switch (typeof(T).Name)
        {
            case nameof(User):
                value = method switch
                {
                    // Here we convert the request body to the entity that we expect to operate on.
                    // Each different Request class method returns a different entity with a specific set of properties of type T.
                    Method.Create => UserRequest.Create(body),
                    Method.Read => UserRequest.Read(body),
                    Method.Update => UserRequest.Update(body),
                    Method.Delete => UserRequest.Delete(body),
                    _ => CustomResponse.Create("error", "Invalid method")
                };
                break;
            
            // Here we will have to add more cases for each different entity we want to operate on.
            // Such as:
            // case nameof(Movie):
            //     value = method switch
            //     {
            //         Method.Create => MovieRequest.Create(body),
            //         Method.Read => MovieRequest.Read(body),
            //         Method.Update => MovieRequest.Update(body),
            //         Method.Delete => MovieRequest.Delete(body),
            //         _ => CustomResponse.Create("error", "Invalid method")
            //     };
            //     break;

            default:
                return CustomResponse.Create("error", "Invalid type");
        }

        if (!Properties(value, method))
            return CustomResponse.Create("error", "Invalid properties");

        return value;
    }
}