using System.Runtime.CompilerServices;
using DevOpsCineMovies.Entities;
using DevOpsCineMovies.Requests;
using Newtonsoft.Json;

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
    /// <param name="memberName"></param>
    /// <typeparam name="T">
    ///     The type of the entity that is being created, read, updated or deleted.
    /// </typeparam>
    /// <returns>
    ///     True if the properties are valid, otherwise false.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private static bool IsPropertiesValid<T>(T obj, string memberName)
    {
        var properties = obj!.GetType().GetProperties();

        foreach (var property in properties)
            if (property.Name == "Id")
                switch (memberName)
                {
                    case "Create":
                        if (property.GetValue(obj) == null) continue;
                        return false;
                    case "Read":
                        if (property.GetValue(obj) != null) continue;
                        return false;
                    case "Update":
                        if (property.GetValue(obj) != null) continue;
                        return false;
                    case "Delete":
                        if (property.GetValue(obj) != null) continue;
                        return false;
                    default:
                        throw new ArgumentOutOfRangeException(memberName, memberName, null);
                }
            else if (!property.GetMethod.IsVirtual)
                switch (memberName)
                {
                    case "Create":
                        if (property.GetValue(obj) != null) continue;
                        return false;
                    case "Read":
                        if (property.GetValue(obj) == null) continue;
                        return false;
                    case "Update":
                        if (property.GetValue(obj) != null) continue;
                        return false;
                    case "Delete":
                        if (property.GetValue(obj) == null) continue;
                        return false;
                    default:
                        throw new ArgumentOutOfRangeException(memberName, memberName, null);
                }

        return true;
    }

    /// <summary>
    ///     This method is used to validate the request body.
    ///     If any errors occur, a CustomResponse object is returned with the error message.
    /// </summary>
    /// <param name="requestBody">
    ///     The request body that is being validated.
    /// </param>
    /// <param name="func"></param>
    /// <param name="memberName"></param>
    /// <typeparam name="T">
    ///     The type of the entity that we expect to operate on.
    /// </typeparam>
    /// <returns>
    ///     The entity that is being created, read, updated or deleted, or a CustomResponse object with the error message.
    /// </returns>
    public static async Task<object> Body<T>(
        Stream requestBody,
        Func<dynamic, T> func,
        [CallerMemberName] string memberName = ""
        ) {
        var requestJson = await new StreamReader(requestBody).ReadToEndAsync();
        var body = JsonConvert.DeserializeObject(requestJson);
        
        if (body is null)
            return CustomResponse.Create("error", "Body is null");

        T value = func(body);

        return (IsPropertiesValid(value, memberName) ? value : CustomResponse.Create("error", "Invalid properties"))!;
    }
}