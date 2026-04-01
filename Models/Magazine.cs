namespace library_system.Models;

public class Magazine : Item
{
    public string Publisher { get; set; } = string.Empty;
    public int Edition { get; set; }
    public string Category { get; set; } = string.Empty;

    public override string GetDescription()
    {
        return $"""
        Title:   {Title}
        Edition:    {Edition}
        Publisher:  {Publisher}
        Category:  {Category}
        Year:      {Year}
        Status:   {(IsAvailable ? "Available" : "Borrowed")}
        """;
    }
}
