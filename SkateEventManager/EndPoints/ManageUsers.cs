using Microsoft.EntityFrameworkCore;

namespace SkateEventManager.EndPoints;

public static class ManageUsers
{
    public static WebApplication GetUsers(this WebApplication app)
    {
        app.MapGet("/currentUser/{id}", async (int id, DatabaseContext db) =>
        {
            var user = db.User.FirstOrDefault(u => u.Id == id);

            return user != null
                ? Results.Ok(user)
                : Results.NotFound("User not found.");
        });

        app.MapGet("/users", async (DatabaseContext db) =>
        {
            var users = await db.User.ToListAsync();
            return users.Any()
                ? Results.Ok(users)
                : Results.NotFound("No users found.");
        });

        return app;
    }
}
