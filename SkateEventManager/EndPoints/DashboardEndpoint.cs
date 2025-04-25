using Microsoft.EntityFrameworkCore;
using SkateEventManager.Models;

namespace SkateEventManager.EndPoints;

public static class DashboardEndpoint
{
    public static WebApplication GetDashboard(this WebApplication app)
    {
        app.MapGet("/dashboard/{id}", async (int id, DatabaseContext db) =>
        {
            var user = await db.User.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return Results.NotFound(new { error = "User not found" });
            }

            var totalReservations = await db.Rent
                .Where(r => r.UserID == user.Id)
                .CountAsync();

            var eventsAttended = await db.Rent
                .Where(r => r.UserID == user.Id && db.Events
                    .Where(e => e.Id == r.EventID && e.EndDate < DateTime.UtcNow)
                    .Any())
                .Select(r => r.EventID)
                .Distinct()
                .CountAsync();

            var activeRentals = await db.Rent
                .Where(r => r.UserID == user.Id && db.Events
                    .Where(e => e.Id == r.EventID && e.StartDate >= DateTime.UtcNow)
                    .Any())
                .CountAsync();

            var activeEvents = await db.Events
                .Where(e => e.StartDate >= DateTime.UtcNow &&
                            db.Rent.Where(r => r.UserID == user.Id)
                                    .Select(r => r.EventID)
                                    .Contains(e.Id))
                .CountAsync();

            //active rentals
            var activeRentalDetail = await db.Rent
                .Where(r => r.UserID == user.Id && db.Events
                .Where(e => e.Id == r.EventID && e.StartDate >= DateTime.UtcNow)
                .Any() && db.Set<Skate>().Where(s => s.Id == r.SkateID).Select(s => s.Size).FirstOrDefault() > 0)
            .Select(r => new
            {
                r.Id,
                Type = db.Set<Skate>().Where(s => s.Id == r.SkateID).Select(s => s.Type).FirstOrDefault(),
                EventStartDate = db.Events.Where(e => e.Id == r.EventID).Select(e => e.StartDate).FirstOrDefault(),
                EventEndDate = db.Events.Where(e => e.Id == r.EventID).Select(e => e.EndDate).FirstOrDefault()
            })
            .ToListAsync();

            //active events for the user
            var activeEventDetail = await db.Rent
                .Where(r => r.UserID == user.Id && db.Events
                .Where(e => e.Id == r.EventID && e.StartDate >= DateTime.UtcNow)
                .Any())
                .Select(r => new
                {
                    EventID = r.Id,
                    EventName = db.Events.Where(e => e.Id == r.EventID).Select(e => e.Name).FirstOrDefault(),
                    EventDate = db.Events.Where(e => e.Id == r.EventID).Select(e => e.StartDate).FirstOrDefault(),
                    SkateSize = db.Set<Skate>().Where(s => s.Id == r.SkateID).Select(s => s.Size).FirstOrDefault()
                })
        .Distinct()
        .ToListAsync();


            var response = new
            {
                felhasznalo = user.Name,
                osszesites = new
                {
                    eddigiFoglalasok = totalReservations,
                    hanyEsemnyenVettelReszt = eventsAttended,
                    aktivBerlesekSzama = activeRentals,
                    aktivEsemenyekSzama = activeEvents
                },
                aktivBerlesek = activeRentalDetail.Select(r => new
                {
                    id = r.Id,
                    termek = "Görkorcsolya, " + r.Type,
                    kezdes = r.EventStartDate.ToString("yyyy-MM-dd"),
                    lejarat = r.EventEndDate.ToString("yyyy-MM-dd")
                }),
                aktivEsemenyek = activeEventDetail.Select(e => new
                {
                    id = e.EventID,
                    esemeny = e.EventName,
                    datum = e.EventDate.ToString("yyyy-MM-dd"),
                    berles = e.SkateSize > 0 ? "Kértél eszközt" : "Saját eszközt hozol"
                })
            };

            return Results.Ok(response);
        });
        return app;
    }
}
