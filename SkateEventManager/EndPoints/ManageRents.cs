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
            var eventExists = await db.Events.AnyAsync(e => e.Id == rent.EventID);
            var skate = await db.Skates.FindAsync(rent.SkateID);
            if (!userExists || !eventExists || skate == null)
            {
                return Results.BadRequest("Invalid User or Event or Skate ID. Does not exist.");
            }


            if (rent.SkateID != 0)
            {
                bool isSkateTaken = await db.Rent.AnyAsync(r => r.SkateID == rent.SkateID && r.EventID == rent.EventID);
                if (isSkateTaken)
                {
                    return Results.BadRequest("This skate is already rented.");
                }
            }

            if (skate.Size != rent.FeetSize)
            {
                return Results.BadRequest($"Feet size mismatch. This skate is size {skate.Size}.");
            }



            db.Rent.Add(rent);
            await db.SaveChangesAsync();

            var response = new
            {
                rent.Id,
                rent.UserID,
                rent.EventID,
                rent.SkateID
            };

            return Results.Created($"/rents/{rent.Id}", response);

        });

        //Update rented skate
        app.MapPut("/rents/{id}", async (int id, Book updatedRent, DatabaseContext db) =>
        {
            var existingRent = await db.Rent.FindAsync(id);
            if (existingRent == null)
            {
                return Results.NotFound("Rent not found.");
            }

            var userExists = await db.User.AnyAsync(u => u.Id == updatedRent.UserID);
            var eventExists = await db.Events.AnyAsync(e => e.Id == updatedRent.EventID);
            var skate = await db.Skates.FindAsync(updatedRent.SkateID);
            if (!userExists || !eventExists || skate == null)
            {
                return Results.BadRequest("Invalid User or Event or Skate ID. Does not exist.");
            }

            if (existingRent.SkateID != 0)
            {
                bool isSkateTaken = await db.Rent.AnyAsync(r => r.SkateID == updatedRent.SkateID && r.EventID == updatedRent.EventID && r.Id != id);
                if (isSkateTaken)
                {
                    return Results.BadRequest("This skate is already rented for this event.");
                }
            }

            if (skate.Size != updatedRent.FeetSize)
            {
                return Results.BadRequest($"Feet size mismatch. This skate is size {skate.Size}.");
            }

            existingRent.UserID = updatedRent.UserID;
            existingRent.EventID = updatedRent.EventID;
            existingRent.FeetSize = updatedRent.FeetSize;
            existingRent.SkateID = updatedRent.SkateID;

            await db.SaveChangesAsync();

            return Results.Ok(existingRent);
        });

        app.MapDelete("/rents/{id}", async (int id, DatabaseContext db) =>
        {
            var rent = await db.Rent.FindAsync(id);
            if (rent is null)
            {
                return Results.NotFound("Rent not found.");
            }

            db.Rent.Remove(rent);
            await db.SaveChangesAsync();

            return Results.Ok("Rent removed successfully.");
        });
        return app;
    }
}
