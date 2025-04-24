using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SkateEventManager;
using SkateEventManager.EndPoints;
using SkateEventManager.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySql(
        "server=localhost;database=skateeventdb;user=root;password=;",
        new MySqlServerVersion(new Version(8, 0, 32))
    ));

builder.Services.AddScoped<UserAuthentication>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("valami_nagyon_hosszu_szoveg_mert_kulonben_nem_engedi_ajajajajajaj"))
        };
    });

builder.Services.AddAuthorization();
var app = builder.Build();

app.CreateSkateItems();
app.ManageSkate();
app.ManageUser();
app.ManageEvenet();
app.ManageRent();
app.GetUsers();
app.GetDashboard();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    var endpoint = context.GetEndpoint();
    var allowAnonymous = endpoint?.Metadata?.GetMetadata<IAllowAnonymous>();

    if (allowAnonymous == null)
    {
        if (!context.User.Identity?.IsAuthenticated ?? true)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }
    }

    await next();
});

app.Run();
