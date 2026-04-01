namespace library_system.Models;

public abstract class Item
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }
    public bool IsAvailable { get; set; } = true;
    public abstract string GetDescription();
        
}
