using SkateEventManager.EndPoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.CreateSkateItems();
app.MapGet("/", () => "Hello World!");

app.Run();
