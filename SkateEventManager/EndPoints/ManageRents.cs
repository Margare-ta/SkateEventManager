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
            var userRents = await db.Rent
                .Where(b => b.UserID == UserId)
                .Include(b => b.Event)
                .Select(b => new
                {
                    EventId = b.Event.Id,
                    EventName = b.Event.Name,
                    StartDate = b.Event.StartDate,
                    EndDate = b.Event.EndDate
                })
                .ToListAsync();

            return userRents.Any()
                ? Results.Ok(userRents)
                : Results.NotFound("No rents found for this user.");
        });

        //Add new rent
        app.MapPost("/rents", async (Book rent, DatabaseContext db) =>
        {
            if (rent == null)
            {
                return Results.BadRequest("Rent data is required.");
            }

            var userExists = await db.User.AnyAsync(u => u.Id == rent.UserID);
            var skate = await db.Skates.FindAsync(rent.SkateID);
            var dbEvent = await db.Events.FindAsync(rent.EventID);
            if (!userExists || skate == null || dbEvent == null)
            {
                return Results.BadRequest("Invalid User or Event or Skate ID. Does not exist.");
            }


            if (rent.FeetSize == 0 || rent.SkateID == 1)
            {
                rent.FeetSize = 0;
                rent.SkateID = 1;
            }
            else
            {
                bool isSkateTaken = await db.Rent.AnyAsync(r => r.SkateID == rent.SkateID && r.EventID == rent.EventID);
                if (isSkateTaken)
                {
                    return Results.BadRequest("This skate is already rented.");
                }
            }

            if (rent.FeetSize != 0 && skate.Size != rent.FeetSize)
            {
                return Results.BadRequest($"Feet size mismatch. This skate is size {skate.Size}.");
            }

            if (dbEvent.AvailablePLaces > 0)
            {
                dbEvent.AvailablePLaces -= 1;
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

            if (updatedRent.FeetSize != 0 && skate.Size != updatedRent.FeetSize)
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
