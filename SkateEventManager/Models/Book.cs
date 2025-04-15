using System.Text.Json.Serialization;

namespace SkateEventManager.Models;

public class Book
{
    public int Id { get; set; }
    public int UserID { get; set; }
    public int EventID { get; set; }
    public double FeetSize { get; set; }
    public int SkateID { get; set; }

    //inline or squad?
    //woman or men??

    // Navigation properties

    [JsonIgnore]
    public User User { get; set; } = null!;
    [JsonIgnore]
    public Event Event { get; set; } = null!;
    [JsonIgnore]
    public Skate Skate { get; set; } = null!;

    public override string? ToString()
    {
        return $"{{ \"Id\": {Id}, \"User\": \"{User}\", \"FeetSize\": \"{FeetSize}\", \"Skate\": \"{Skate}\" }}";
    }
}
