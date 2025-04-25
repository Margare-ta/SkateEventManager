using Microsoft.EntityFrameworkCore;

namespace SkateEventManager.EndPoints;

public static class ManageSkates
{
    public static void ManageSkate(this WebApplication app)
    {
        // Get all skates
        app.MapGet("/skates", async (DatabaseContext db) =>
        {
            var skates = await db.Skates.ToListAsync();
            return skates.Count > 0 ? Results.Ok(skates) : Results.NotFound("No skates found.");
        });

        //Get available  Skates
        app.MapGet("/availableSkates", async (DatabaseContext db) =>
        {
            var now = DateTime.UtcNow;

            var rentedSkateIds = await db.Rent
                .Where(rent => db.Events
                    .Any(e => e.Id == rent.EventID && e.EndDate > now))
                .Select(rent => rent.SkateID)
                .ToListAsync();

            var availableSkates = await db.Skates
                .Where(skate => !rentedSkateIds.Contains(skate.Id))
                .ToListAsync();

            return availableSkates.Any()
                ? Results.Ok(availableSkates)
                : Results.NotFound("No skates found.");
        });

        // Get skate by id
        app.MapGet("/skates/{id}", async (int id, DatabaseContext db) =>
        {
            try
            {
                var skate = await db.Skates.FindAsync(id);
                return skate is not null ? Results.Ok(skate) : Results.NotFound("Skate not found.");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        });

        // Delete skate by id
        app.MapDelete("/skates/{id}", async (int id, DatabaseContext db) =>
        {
            var skate = await db.Skates.FindAsync(id);
            if (skate is null)
            {
                return Results.NotFound("Skate not found.");
            }

            db.Skates.Remove(skate);
            await db.SaveChangesAsync();

            return Results.Ok("Skate removed successfully.");
        });
    }
}
