namespace SkateEventManager.EndPoints;

public static class DashboardEndpoint
{
    public static WebApplication GetDashboard(this WebApplication app)
    {
        app.MapGet("/dashboard/{id}", async (String id, DatabaseContext db) =>
        {

        });
        return app;
    }
}
