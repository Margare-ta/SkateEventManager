namespace SkateEventManager.EndPoints;

public static class ManageUsers
{
    public static WebApplication GetUsers(this WebApplication app)
    {
        app.MapGet("/currentUser/{id}", (int id, DatabaseContext db) =>
        {
            var user = db.User.FirstOrDefault(u => u.Id == id);

            return user != null
                ? Results.Ok(user)
                : Results.NotFound("User not found.");
        });

        return app;
    }
}
