namespace SkateEventManager.Services;
using BCrypt.Net;
using SkateEventManager.Enums;
using SkateEventManager.Models;
using System.Linq;

public class UserAuthentication
{
    private readonly DatabaseContext _context;

    public UserAuthentication(DatabaseContext context)
    {
        _context = context;
    }

    public void RegisterUser(string name, string email, string password, RoleOfUser role)
    {
        string passwordHash = BCrypt.HashPassword(password);

        var user = new User { Name = name, Email = email, PasswordInHash = passwordHash, Role = role };
        _context.User.Add(user);
        _context.SaveChanges();
    }

    public int? LoginUser(string email, string password)
    {
        var user = _context.User.FirstOrDefault(u => u.Email == email);
        if (user == null) return null; // User not found

        return BCrypt.Verify(password, user.PasswordInHash) ? user.Id : null;
    }
}
//validáció
//titkosítás
