namespace library_system.Models;

public class Book : Item
{
    public string Author { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public int Pages { get; set; }

    public override string GetDescription()
    {
        return $"""
        Title:   {Title}
        Author:    {Author}
        ISBN:     {Isbn}
        Publisher:  {Publisher}
        Pages:  {Pages}
        Year:      {Year}
        Status:   {(IsAvailable ? "Available" : "Borrowed")}
        """;
    }

}
