namespace SkateEventManager.EndPoints;

public static class ManageUsers
{
    public static WebApplication GetUsers(this WebApplication app)
    {
        app.MapGet("/currentUser/{id}", (string id, DatabaseContext db) =>
        {
            if (!int.TryParse(id, out int userId))
            {
                return "Invalid ID format.";
            }

            string? searchedUser = null;
            var user = db.User
                           .Where(item => item.Id == userId);

            foreach (var item in user)
            {
                searchedUser += item.ToString();
            }
            return searchedUser is not null ? searchedUser : "User not found.";
        });

        return app;
    }
}
