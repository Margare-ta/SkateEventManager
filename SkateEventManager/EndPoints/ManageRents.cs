using Microsoft.EntityFrameworkCore;
using SkateEventManager.Models;

namespace SkateEventManager.EndPoints;

public static class ManageRents
{
    public static WebApplication ManageRent(this WebApplication app)
    {
        //Get rents in json
        app.MapGet("/rents", async (DatabaseContext db) =>
        {
            var rents = await db.Skates.ToListAsync();
            return rents.Count > 0 ? Results.Ok(rents) : Results.NotFound("No rents found.");
        });

        //Get rent by UserId
        app.MapGet("/rents/{UserId}", async (int UserId, DatabaseContext db) =>
        {
            var searchRent = await db.Events.FindAsync(UserId);
            return searchRent is not null ? Results.Ok(searchRent) : Results.NotFound("No rent for this user.");
        });

        //Add new rent
        app.MapPost("/rents", async (Book rent, DatabaseContext db) =>
        {
            if (rent == null)
            {
                return Results.BadRequest("Rent data is required.");
            }

            var userExists = await db.User.AnyAsync(u => u.Id == rent.UserID);
            if (!userExists)
            {
                return Results.BadRequest("Invalid UserID. User does not exist.");
            }

            var eventExists = await db.Events.AnyAsync(e => e.Id == rent.EventID);
            if (!eventExists)
            {
                return Results.BadRequest("Invalid EventID. Event does not exist.");
            }

            var skate = await db.Skates.FindAsync(rent.SkateID);
            if (skate == null)
            {
                return Results.BadRequest("Invalid SkateID. Skate does not exist.");
            }

            bool isSkateTaken = await db.Rent.AnyAsync(r => r.SkateID == rent.SkateID);
            if (isSkateTaken)
            {
                return Results.BadRequest("This skate is already rented.");
            }

            if (skate.Size != rent.FeetSize)
            {
                return Results.BadRequest($"Feet size mismatch. This skate is size {skate.Size}.");
            }

            db.Rent.Add(rent);
            await db.SaveChangesAsync();

            return Results.Created($"/rents/{rent.Id}", rent);

        });
        return app;
    }
}
