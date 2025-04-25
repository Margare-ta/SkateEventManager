using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SkateEventManager.Models;

namespace SkateEventManager.EndPoints;

public static class ManageEvents
{
    public static WebApplication ManageEvenet(this WebApplication app)
    {

        //Get events in json
        app.MapGet("/events", [AllowAnonymous] async (DatabaseContext db) =>
        {
            var events = await db.Events.ToListAsync();
            return events.Count > 0 ? Results.Ok(events) : Results.NotFound("No event found.");
        });

        //Get event by Id
        app.MapGet("/events/{id}", [AllowAnonymous] async (int id, DatabaseContext db) =>
        {
            var searchEdvent = await db.Events.FindAsync(id);
            return searchEdvent is not null ? Results.Ok(searchEdvent) : Results.NotFound("Event not found.");
        });

        //Get event by UserId


        //Add new event
        app.MapPost("/events", async (Event newEvent, DatabaseContext db) =>
        {
            if (newEvent == null)
            {
                return Results.BadRequest("Event data is required.");
            }

            if (newEvent.AvailablePLaces < newEvent.Reserved)
            {
                return Results.BadRequest("Available places cannot be less than reserved places.");
            }


            db.Events.Add(newEvent);
            await db.SaveChangesAsync();

            return Results.Created();
        });

        //Delete Event
        app.MapDelete("/events/{id}", async (int id, DatabaseContext db) =>
        {
            var newEvent = await db.Events.FindAsync(id);
            if (newEvent is null)
            {
                return Results.NotFound("Event not found.");
            }

            db.Events.Remove(newEvent);
            await db.SaveChangesAsync();

            return Results.Ok("Event removed successfully.");
        });
        return app;
    }
}
