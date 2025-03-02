namespace SkateEventManager.Models;

public class Book
{
    public int Id { get; set; }
    public int UserID { get; set; }
    public int EventID { get; set; }
    public double FeetSize { get; set; }
    public int SkateID { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public Event Event { get; set; } = null!;
    public Skate Skate { get; set; } = null!;

    public override string? ToString()
    {
        return $"{{ \"Id\": {Id}, \"User\": \"{User}\", \"FeetSize\": \"{FeetSize}\", \"Skate\": \"{Skate}\" }}";
    }
}
