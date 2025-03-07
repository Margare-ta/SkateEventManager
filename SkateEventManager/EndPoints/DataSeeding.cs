using SkateEventManager.Enums;
using SkateEventManager.Models;

namespace SkateEventManager.EndPoints;

public static class DataSeeding
{
    public static WebApplication CreateSkateItems(this WebApplication app)
    {

        app.MapGet("/seedData", () =>
        {
            using var db = new DatabaseContext();


            var skates = new List<Skate>
            {

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

            db.SaveChanges();

            int skatesInDb = db.Skates.ToList().Count;
            if (skatesInDb == skates.Count)
            {
                return Results.Ok("Database correctly seeded.");
            }
            else
            {
                return Results.Problem("Data is not saved correctly");
            }

        });

        return app;
    }
}

