using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.model.dto;
using WebApplication1.setvice;

namespace WebApplication1.auth;

public class UserEndPoints
{
    public static IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("register", Register);
        endpoints.MapPost("login", Login);

        endpoints.MapGet("/secure", [Authorize]() => Results.Ok("Access granted"));


        return endpoints;
    }

    private static async Task<IResult> Register(UserRegisterDto request, [FromServices] IUserService userService)
    {
        await userService.Register(request.Username, request.Email, request.Password);
        return Results.Ok();
    }

    private static async Task<IResult> Login(UserLoginDto request, [FromServices] IUserService userService,
        HttpContext httpContext)
    {
        var token = await userService.Login(request.Email, request.Password);
        httpContext.Response.Cookies.Append("tasty-cookies", token);

        return Results.Ok(token);
    }

}