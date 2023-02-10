using DevOpsCineMovies.Context;
using DevOpsCineMovies.Entities;
using DevOpsCineMovies.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevOpsCineMovies.Controllers;

/// <summary>
///     Controller for the User entity with CRUD operations.
///     Every method returns a custom response, which is a JSON object with the following structure:
///     {
///     "status": "success" | "error",
///     "message": "message",
///     "data": "data" (aka the entity)
///     }
///     The data field is optional and is only present when the status is "success".
///     Every method also validates the request body using the Validator class.
///     If the request body is invalid, the method returns a custom response with the error message.
///     If the request body is valid, the method returns the entity.
///     The entity is then used to perform the operation.
///     If the operation is successful, the method returns a custom response with the entity.
///     If the operation is not successful, the method returns a custom response with the error message.
///     Every method must be called with a POST request, otherwise it will not work.
/// </summary>
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly MyDbContext _context = new();

    [HttpPost]
    [Route(nameof(Create))]
    public async Task<object> Create()
    {
        var response = await Validator.Body<User>(Request.Body, Method.Create);
        if (response is not User user)
            return response;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CustomResponse.Create("success", "User created", user);
    }

    [HttpPost]
    [Route(nameof(Read))]
    public async Task<object> Read()
    {
        var response = await Validator.Body<User>(Request.Body, Method.Read);
        if (response is not User user)
            return response;

        return await _context.Users.FindAsync(user.Id) is { } foundUser
            ? CustomResponse.Create("success", "User found", foundUser)
            : CustomResponse.Create("error", "User not found");
    }

    [HttpPost]
    [Route(nameof(Update))]
    public async Task<object> Update()
    {
        var response = await Validator.Body<User>(Request.Body, Method.Update);
        if (response is not User user)
            return response;

        var dbUser = await _context.Users.FindAsync(user.Id);
        if (dbUser is null)
            return CustomResponse.Create("error", "User not found");

        dbUser.Name = user.Name;
        dbUser.Phone = user.Phone;
        dbUser.Email = user.Email;
        dbUser.Password = user.Password;
        await _context.SaveChangesAsync();

        return CustomResponse.Create("success", "User updated", user);
    }

    [HttpPost]
    [Route(nameof(Delete))]
    public async Task<object> Delete()
    {
        var response = await Validator.Body<User>(Request.Body, Method.Delete);
        if (response is not User user)
            return response;

        var dbUser = await _context.Users.FindAsync(user.Id);
        if (dbUser is null)
            return CustomResponse.Create("error", "User not found");

        _context.Users.Remove(dbUser);
        await _context.SaveChangesAsync();

        return CustomResponse.Create("success", "User deleted", user);
    }
}