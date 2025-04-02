using SkateEventManager.Enums;
using System.Text.Json.Serialization;

namespace SkateEventManager.Models;

public class Skate
{
    public int Id { get; set; }
    public double Size { get; set; }
    public GenderCategory Gender { get; set; }
    public TypeCategory Type { get; set; }

    // Navigation Property (One Event → Many Books)
    [JsonIgnore]
    public List<Book> Books { get; set; } = new();

    public override string ToString()
    {
        return $"{{ \"Id\": {Id}, \"Size\": \"{Size}\", \"Gender\": \"{Gender}\", \"Type\": \"{Type}\" }}";
    }
}
