﻿using Microsoft.EntityFrameworkCore;
using SkateEventManager.Enums;
using SkateEventManager.Models;

namespace SkateEventManager.EndPoints;

public static class DataSeeding
{
    public static WebApplication CreateSkateItems(this WebApplication app)
    {

        app.MapGet("/seedData", async (DatabaseContext db) =>
        {
            var skates = new List<Skate>
            {
                new Skate {Size = 0, Gender = GenderCategory.Unisex, Type = TypeCategory.Quad },//for own skate
                new Skate { Size = 38, Gender = GenderCategory.Men, Type = TypeCategory.Inline },
                new Skate { Size = 39, Gender = GenderCategory.Women, Type = TypeCategory.Quad },
                new Skate { Size = 40, Gender = GenderCategory.Unisex, Type = TypeCategory.Quad },
                new Skate { Size = 41, Gender = GenderCategory.Women, Type = TypeCategory.Quad },
                new Skate { Size = 42, Gender = GenderCategory.Men, Type = TypeCategory.Inline },
                new Skate { Size = 42, Gender = GenderCategory.Men, Type = TypeCategory.Inline },
                new Skate { Size = 43, Gender = GenderCategory.Women, Type = TypeCategory.Quad },
                new Skate { Size = 44, Gender = GenderCategory.Unisex, Type = TypeCategory.Quad },
                new Skate { Size = 45, Gender = GenderCategory.Women, Type = TypeCategory.Quad },
                new Skate { Size = 46, Gender = GenderCategory.Unisex, Type = TypeCategory.Inline },
                new Skate { Size = 37, Gender = GenderCategory.Women, Type = TypeCategory.Quad },
                new Skate { Size = 38, Gender = GenderCategory.Unisex, Type = TypeCategory.Quad },
                new Skate { Size = 39, Gender = GenderCategory.Women, Type = TypeCategory.Quad },
                new Skate { Size = 40, Gender = GenderCategory.Unisex, Type = TypeCategory.Inline },
                new Skate { Size = 41, Gender = GenderCategory.Women, Type = TypeCategory.Quad },
                new Skate { Size = 42, Gender = GenderCategory.Unisex, Type = TypeCategory.Quad },
                new Skate { Size = 43, Gender = GenderCategory.Women, Type = TypeCategory.Quad },
                new Skate { Size = 44, Gender = GenderCategory.Unisex, Type = TypeCategory.Inline },
                new Skate { Size = 45, Gender = GenderCategory.Women, Type = TypeCategory.Quad },
                new Skate { Size = 46, Gender = GenderCategory.Unisex, Type = TypeCategory.Quad },
                new Skate { Size = 37, Gender = GenderCategory.Women, Type = TypeCategory.Quad },
                new Skate { Size = 38, Gender = GenderCategory.Unisex, Type = TypeCategory.Inline },
                new Skate { Size = 39, Gender = GenderCategory.Women, Type = TypeCategory.Quad },
                new Skate { Size = 40, Gender = GenderCategory.Unisex, Type = TypeCategory.Quad },
                new Skate { Size = 41, Gender = GenderCategory.Women, Type = TypeCategory.Quad },
                new Skate { Size = 42, Gender = GenderCategory.Unisex, Type = TypeCategory.Inline },
                new Skate { Size = 43, Gender = GenderCategory.Women, Type = TypeCategory.Quad },
                new Skate { Size = 44, Gender = GenderCategory.Unisex, Type = TypeCategory.Quad },
                new Skate { Size = 45, Gender = GenderCategory.Women, Type = TypeCategory.Quad },
                new Skate { Size = 46, Gender = GenderCategory.Unisex, Type = TypeCategory.Inline },
                new Skate { Size = 37, Gender = GenderCategory.Women, Type = TypeCategory.Quad }
            };
            db.Skates.AddRange(skates);

            //Add admin and one user
            var adminUser = new User { Name = "Admin user", Email = "adminUser@gmail.com", PasswordInHash = "strongPassword123", Role = RoleOfUser.Admin };
            var user = new User { Name = "Normal user", Email = "normalUser@gmail.com", PasswordInHash = "easy", Role = RoleOfUser.Normal };
            db.User.Add(adminUser);
            db.User.Add(user);

            //Add new events
            var events = new List<Event>
            {
                new Event {StartDate = new DateTime(2025,04,02), EndDate = new DateTime(2025,04,03), Name = "Skate Ring at Liget", Accommodation = 0, AvailablePLaces= 120, Reserved = 22},
                new Event {StartDate = new DateTime(2025,06,09), EndDate= new DateTime(2025,06,10), Name = "Rolling Disco", Accommodation = 0, AvailablePLaces = 501, Reserved = 70},
                new Event {StartDate = new DateTime(2025,07,22), EndDate= new DateTime(2025,07,22), Name = "Skate under the starry sky", Accommodation = 0, AvailablePLaces = 105, Reserved = 6}

            };
            db.Events.AddRange(events);

            await db.SaveChangesAsync();

            int skatesInDb = await db.Skates.CountAsync();
            return skatesInDb == skates.Count
                ? Results.Ok("Database correctly seeded.")
                : Results.Problem("Data is not saved correctly");

        });

        return app;
    }
}

