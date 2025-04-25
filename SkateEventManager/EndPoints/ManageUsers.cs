using Microsoft.EntityFrameworkCore;
using SkateEventManager.Dtos;
using System.Security.Claims;

namespace SkateEventManager.EndPoints;

public static class ManageUsers
{
    public static WebApplication GetUsers(this WebApplication app)
    {
        //Get user by id
        app.MapGet("/currentUser/{id}", async (int id, DatabaseContext db) =>
        {
            var user = db.User.FirstOrDefault(u => u.Id == id);

            return user != null
                ? Results.Ok(user)
                : Results.NotFound("User not found.");
        });

        //Get all user
        app.MapGet("/users", async (DatabaseContext db) =>
        {
            var users = await db.User.ToListAsync();
            return users.Any()
                ? Results.Ok(users)
                : Results.NotFound("No users found.");
        });

        //Change users data
        app.MapPut("/users", async (HttpContext http, UpdateUserDto dto, DatabaseContext db) =>
        {
            var userIdClaim = http.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
                return Results.Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var user = await db.User.FindAsync(userId);
            if (user is null)
                return Results.NotFound("User not found.");

            if (!string.IsNullOrWhiteSpace(dto.Name))
                user.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                user.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.NewPassword))
            {
                user.PasswordInHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            }

            await db.SaveChangesAsync();

            return Results.Ok("User updated successfully.");
        });


        return app;
    }
}
