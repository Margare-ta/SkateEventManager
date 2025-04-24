using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using SkateEventManager.Models;
using SkateEventManager.Services;
using System.Security.Claims;
using System.Text;

namespace SkateEventManager.EndPoints;

public static class AuthenticationEndpoints
{
    public static void ManageUser(this WebApplication app)//new register, email passwords
    {
        app.MapPost("/api/auth/register", [AllowAnonymous] (UserAuthentication authService, ExtendedRegisterRequest request) =>
        {
            authService.RegisterUser(request.Name, request.RegisterRequest.Email, request.RegisterRequest.Password, request.Role);
            return Results.Ok(new { message = "User registered successfully!" });
        });

        app.MapPost("/api/auth/login", [AllowAnonymous] (UserAuthentication authService, LoginRequest request) =>
        {
            int? userId = authService.LoginUser(request.Email, request.Password);
            if (userId == null) return Results.Unauthorized();

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("valami_nagyon_hosszu_szoveg_mert_kulonben_nem_engedi_ajajajajajaj");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId.ToString()!) }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Results.Ok(new
            {
                message = "Login successful!",
                token = tokenString
            });
        });
    }
}
