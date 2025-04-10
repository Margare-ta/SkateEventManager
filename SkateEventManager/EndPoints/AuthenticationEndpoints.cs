using Microsoft.AspNetCore.Identity.Data;
using SkateEventManager.Models;
using SkateEventManager.Services;

namespace SkateEventManager.EndPoints;

public static class AuthenticationEndpoints
{
    public static void ManageUser(this WebApplication app)//new register, email passwords
    {
        app.MapPost("/api/auth/register", (UserAuthentication authService, ExtendedRegisterRequest request) =>
        {
            authService.RegisterUser(request.Name, request.RegisterRequest.Email, request.RegisterRequest.Password, request.Role);
            return Results.Ok(new { message = "User registered successfully!" });
        });

        app.MapPost("/api/auth/login", (UserAuthentication authService, LoginRequest request) =>
        {
            int? userId = authService.LoginUser(request.Email, request.Password);
            if (userId == null) return Results.Unauthorized();
            return Results.Ok(new
            {
                message = "Login successful!",
                userId = userId
            });
        });
    }
}
