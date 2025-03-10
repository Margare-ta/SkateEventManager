namespace SkateEventManager.EndPoints;

public static class ManageSkates
{
    public static WebApplication ManageSkate(this WebApplication application)
    {
        //get all skate
        application.MapGet("/skates", () =>
        {
            string skateData = "";
            using var db = new DatabaseContext();

            var skates = db.Skates
                            .ToList();
            foreach (var item in skates)
            {
                skateData += item.ToString();
            }
            return skateData;
        });

        //get skate by id
        application.MapGet("/skates/{id}", (string id) =>
        {
            if (!int.TryParse(id, out int skateId))
            {
                return "Invalid ID format.";
            }

            string? searchedSkate = null;
            using var db = new DatabaseContext();
            var skates = db.Skates
                           .Where(item => item.Id == skateId);

            foreach (var item in skates)
            {
                searchedSkate += item.ToString();
            }
            return searchedSkate is not null ? searchedSkate : "Skate not found.";
        });

        return application;
    }
}
