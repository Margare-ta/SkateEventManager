namespace SkateEventManager.Models;

public class Event
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public required string Name { get; set; }
    public int AvailablePLaces { get; set; }
    public int Accommodation { get; set; }
    public int Reserved { get; set; }//ez meg mi???

    // Navigation Property (One Event → Many Books)
    public List<Book> Books { get; set; } = new();

    public override string ToString()
    {
        return $"{{ \"Id\": {Id}, \"StartDate\": \"{StartDate}\", \"Name\": \"{Name}\", \"Accommodation\": \"{Accommodation}\", \"AvaiblePLaces\": \"{AvailablePLaces}\", \"Reserved\": \"{Reserved}\" }}";
    }
}
