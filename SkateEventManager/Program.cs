using Microsoft.EntityFrameworkCore;
using SkateEventManager;
using SkateEventManager.EndPoints;
using SkateEventManager.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySql(
        "server=localhost;database=skateeventdb;user=root;password=;",
        new MySqlServerVersion(new Version(8, 0, 32))
    ));

builder.Services.AddScoped<UserAuthentication>();

var app = builder.Build();

app.CreateSkateItems();
app.ManageSkate();
app.ManageUser();
app.ManageEvenet();
app.ManageRent();
app.GetUsers();
app.GetDashboard();
app.MapGet("/", () => "Hello World!");


app.Run();
