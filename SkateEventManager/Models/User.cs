using SkateEventManager.Enums;

namespace SkateEventManager.Models;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PasswordInHash { get; set; }
    public RoleOfUser Role { get; set; }


    // Navigation property
    public List<Book> Books { get; set; } = new();

    public override string ToString()
    {
        return $"{{ \"Id\": {Id}, \"Name\": \"{Name}\", \"Email\": \"{Email}\", \"Role\": \"{Role}\" }}";
    }
}
